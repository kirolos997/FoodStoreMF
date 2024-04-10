using FluentAssertions;
using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.DTO.QueryFilters;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Generic;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Products.v1;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using Moq;
using System.Linq.Expressions;

namespace FoodStore.ServiceTests.Products.v1
{
    public class ProductGetterTest
    {

        private readonly Mock<IProductsRepository> _productsRepositoryMockFactory;
        private readonly IProductsRepository _productsRepository;

        private readonly IProductsGetterService _productsGetterService;
        public ProductGetterTest()
        {
            // Mocking what the ProductGetterService depends on
            _productsRepositoryMockFactory = new Mock<IProductsRepository>();

            // Getting the mocked object
            _productsRepository = _productsRepositoryMockFactory.Object;

            // XUnit does not provide the concept of dependency injection.Hence, create the object 
            _productsGetterService = new ProductsGetterService(_productsRepository);
        }

        #region GetAllProucts
        [Fact]
        public async Task GetAllProducts_ToBeProductList()
        {
            //Arrange
            Pagination pagination = new Pagination(); //  default pagination object

            List<Product> products = [new Product(), new Product()]; // simple list with just two product in it

            // Mocking logic: Whenever we call "GetAllProducts" with any pagination object and any lambda,
            // it should return the specified return value

            _productsRepositoryMockFactory
                .Setup(temp => temp.GetAllProducts(It.IsAny<Pagination>(), It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            //Act
            List<ProductResponse> products_from_get = await _productsGetterService.GetAllProducts(pagination, null);

            //Assert
            products.Should().BeEquivalentTo(products_from_get);
        }
        [Fact]
        public async Task GetAllProducts_ToBeEmptyList()
        {
            //Arrange
            Pagination pagination = new Pagination(); //  default pagination object

            List<Product> products = []; // Empty list

            // Mocking logic: Whenever we call "GetAllProducts" with any pagination object and any lambda,
            // it should return the specified return value

            _productsRepositoryMockFactory
                .Setup(temp => temp.GetAllProducts(It.IsAny<Pagination>(), It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            //Act
            List<ProductResponse> products_from_get = await _productsGetterService.GetAllProducts(pagination, null);

            //Assert
            products_from_get.Should().BeEmpty();
        }
        [Fact]
        public async Task GetAllProducts_InvalidOperator_ToBeInvalidOperatorException()
        {
            //Arrange
            Pagination pagination = new Pagination(); //  default pagination object
            FilterOptions<ProductResponse>? searchOptions = new FilterOptions<ProductResponse>() { Search = ["productname eqq p1"] };

            //Act
            Func<Task> action = async () =>
            {
                await _productsGetterService.GetAllProducts(pagination, searchOptions);

            };

            //Assert
            await action.Should().ThrowAsync<InvalidOperatorException>();

        }
        [Fact]
        public async Task GetAllProducts_InvalidPriceValue_ToBeInvalidOperationException()
        {
            //Arrange
            Pagination pagination = new Pagination(); // 
            FilterOptions<ProductResponse>? searchOptions = new FilterOptions<ProductResponse>() { Search = ["price eq 10p"] };

            Func<Task> action = async () =>
            {
                await _productsGetterService.GetAllProducts(pagination, searchOptions);

            };

            //Assert
            await action.Should().ThrowAsync<InvalidOperationException>();

        }
        [Fact]
        public async Task GetAllProducts_InvalidInStoreValue_ToBeInvalidOperationException()
        {
            //Arrange
            Pagination pagination = new Pagination(); // 
            FilterOptions<ProductResponse>? searchOptions = new FilterOptions<ProductResponse>() { Search = ["instore eq trruee"] };

            var p1 = new Product() { ProductName = "p1" };

            List<Product> products = [p1]; //  product list

            // Mocking logic: Whenever we call "GetAllProducts" with the given pagination object and any lambda,
            // it should return the specified return value

            _productsRepositoryMockFactory
                .Setup(temp => temp.GetAllProducts(It.IsAny<Pagination>(), It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            //Act
            Func<Task> action = async () =>
            {
                await _productsGetterService.GetAllProducts(pagination, searchOptions);

            };

            //Assert
            await action.Should().ThrowAsync<InvalidOperationException>();

        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetProductByID_NullProductID_ToBeArgumentNullException()
        {
            //Arrange
            Guid? productID = null;

            //Act
            Func<Task> action = async () =>
            {

                await _productsGetterService.GetProductByProductID(productID);

            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();

        }
        [Fact]
        public async Task GetProductByID_ValidProductID_ToValidProduct()
        {
            //Arrange
            Guid productID = Guid.NewGuid();

            Product product = new Product()
            {
                ProductName = "p1",
                CategoryId = Guid.NewGuid(),
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductId = productID,
                Category = new Category() { }
            };
            ProductResponse arrangeProductResponse = product.ToProductResponse();

            // Mocking logic: Whenever we call "GetProductByID" with any GUID,
            // it should return the specified return value

            _productsRepositoryMockFactory.Setup(temp => temp.GetProductByID(It.IsAny<Guid>())).ReturnsAsync(product);

            //Act
            ProductResponse? product_from_get = await _productsGetterService.GetProductByProductID(productID);

            //Assert
            product_from_get.ProductId.Should().Be(arrangeProductResponse.ProductId);

        }
        #endregion


    }
}

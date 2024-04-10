using FluentAssertions;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Categories.v1;
using FoodStore.Core.Services.Products.v1;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using Moq;

namespace FoodStore.ServiceTests.Products.v1
{
    /// <summary>
    ///  Unit testing for ProductUpdaterService V1
    /// </summary>
    public class ProductsUpdaterTests
    {
        private readonly IProductsUpdaterService _productsUpdaterService;
        private readonly ICategoriesGetterService _categoriesGetterService;

        private readonly Mock<IProductsRepository> _productsRepositoryMockFactory;

        private readonly IProductsRepository _productsRepository;

        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMockFactory;

        private readonly ICategoriesRepository _categoriesRepository;
        public ProductsUpdaterTests()
        {
            // Mocking what the ProductAdderService depends on
            _productsRepositoryMockFactory = new Mock<IProductsRepository>();
            _categoriesRepositoryMockFactory = new Mock<ICategoriesRepository>();

            // Getting the mocked object
            _productsRepository = _productsRepositoryMockFactory.Object;
            _categoriesRepository = _categoriesRepositoryMockFactory.Object;

            // XUnit does not provide the concept of dependency injection.Hence, create the object
            _categoriesGetterService = new CategoriesGetterService(_categoriesRepository);
            _productsUpdaterService = new ProductsUpdaterService(_productsRepository, _categoriesGetterService);
        }

        [Fact]
        public async Task UpdateProduct_NullProductID_ToBeArgumentNullException()
        {
            //Arrange
            Guid? productID = null;
            ProductUpdateRequest productUpdateRequest = new ProductUpdateRequest()
            {
                CategoryId = Guid.NewGuid(),
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductId = Guid.NewGuid(),
                ProductName = "P1"

            };

            //Act
            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productID, productUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProduct_NullProductRequest_ToBeArgumentNullException()
        {
            //Arrange
            Guid productID = Guid.NewGuid();
            ProductUpdateRequest? productUpdateRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productID, productUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProduct_InvalidProductID_ToBeInvalidProductIDException()
        {
            //Arrange
            Guid? productID = Guid.NewGuid();

            ProductUpdateRequest productUpdateRequest = new ProductUpdateRequest()
            {
                CategoryId = Guid.NewGuid(),
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductId = Guid.NewGuid(),
                ProductName = "P1"

            };

            Product product = null;

            Category Category = new Category() { CategoryId = Guid.NewGuid(), Name = "c1", products = [] };


            // Mocking logic: Whenever we call "GetCategoryByID" with any string,
            // it should return the specified return value
            _categoriesRepositoryMockFactory.Setup(temp => temp.GetCategoryByID(It.IsAny<Guid>())).ReturnsAsync(Category);


            // Mocking logic: Whenever we call "UpdateProduct" with any string,
            // it should return the specified return value
            _productsRepositoryMockFactory.Setup(temp => temp.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).ReturnsAsync(product);


            //Act
            Func<Task> action = async () =>
            {
                await _productsUpdaterService.UpdateProduct(productID, productUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<InvalidProductIDException>();
        }
        [Fact]
        public async Task UpdateProduct_ValidProduct_ToBeSuccessfullyUpdated()
        {
            //Arrange
            Guid? productID = Guid.NewGuid();

            ProductUpdateRequest productUpdateRequest = new ProductUpdateRequest()
            {
                CategoryId = Guid.NewGuid(),
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductId = Guid.NewGuid(),
                ProductName = "P1"

            };

            Product product = productUpdateRequest.ToProduct();

            Category Category = new Category() { CategoryId = Guid.NewGuid(), Name = "c1", products = [] };

            // Mocking logic: Whenever we call "GetCategoryByID" with any string,
            // it should return the specified return value
            _categoriesRepositoryMockFactory.Setup(temp => temp.GetCategoryByID(It.IsAny<Guid>())).ReturnsAsync(Category);

            // Mocking logic: Whenever we call "UpdateProduct" with any string,
            // it should return the specified return value
            _productsRepositoryMockFactory.Setup(temp => temp.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).ReturnsAsync(product);


            //Act
            ProductResponse productRespone_get = await _productsUpdaterService.UpdateProduct(productID, productUpdateRequest);

            //Assert
            productRespone_get.ProductId.Should().Be(productUpdateRequest.ProductId);
        }


    }
}

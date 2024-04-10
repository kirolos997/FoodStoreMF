using FluentAssertions;
using FoodStore.Application.DTO.Categories.v2;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Categories.v1;
using FoodStore.Core.Services.Products;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using FoodStore.Core.ServicesContracts.IProducts;
using Moq;

namespace FoodStore.ServiceTests.Products.v1
{
    public class ProductsAdder
    {
        private readonly IProductsAdderService _productsAdderService;
        private readonly ICategoriesGetterService _categoriesGetterService;

        private readonly Mock<IProductsRepository> _productsRepositoryMockFactory;

        private readonly IProductsRepository _productsRepository;

        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMockFactory;

        private readonly ICategoriesRepository _categoriesRepository;
        public ProductsAdder()
        {
            // Mocking what the ProductAdderService depends on
            _productsRepositoryMockFactory = new Mock<IProductsRepository>();
            _categoriesRepositoryMockFactory = new Mock<ICategoriesRepository>();

            // Getting the mocked object
            _productsRepository = _productsRepositoryMockFactory.Object;
            _categoriesRepository = _categoriesRepositoryMockFactory.Object;

            // XUnit does not provide the concept of dependency injection.Hence, create the object
            _categoriesGetterService = new CategoriesGetterService(_categoriesRepository);
            _productsAdderService = new ProductsAdderService(_productsRepository, _categoriesGetterService);
        }

        [Fact]
        public async Task AddProduct_NullProduct_ToBeArgumentNullException()
        {
            //Arrange
            ProductAddRequest? productAddRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _productsAdderService.AddProduct(productAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddProduct_DuplicateProductName_ToDuplicateProductException()
        {
            //Arrange
            ProductAddRequest? productAddRequest = new ProductAddRequest()
            {
                CategoryId = Guid.NewGuid()
            ,
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductName = "p1"
            };

            Product product = productAddRequest.ToProduct();

            // Mocking logic: Whenever we call "GetProductByName" with any string,
            // it should return the specified return value
            _productsRepositoryMockFactory.Setup(temp => temp.GetProductByName(It.IsAny<string>())).ReturnsAsync(product);

            //Act
            Func<Task> action = async () =>
            {
                await _productsAdderService.AddProduct(productAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<DuplicateProductException>();
        }

        [Fact]
        public async Task AddProduct_NewProductName_ToBeSuccessfullyAdded()
        {
            //Arrange
            ProductAddRequest? productAddRequest = new ProductAddRequest()
            {
                CategoryId = Guid.NewGuid()
            ,
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductName = "p1"
            };
            CategoryResponse categoryResponse = new CategoryResponse()
            {
                CategoryID = Guid.NewGuid(),
                Name = "",
                Products = []
            };

            Product product = productAddRequest.ToProduct();

            Product? nullProduct = null;

            Category category = categoryResponse.ToCategory();


            // Mocking logic: Whenever we call "GetProductByName" with any string,
            // it should return the specified return value
            _productsRepositoryMockFactory.Setup(temp => temp.GetProductByName(It.IsAny<string>())).ReturnsAsync(nullProduct);

            // Mocking logic: Whenever we call "AddProduct" with any string,
            // it should return the specified return value
            _productsRepositoryMockFactory.Setup(temp => temp.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

            // Mocking logic: Whenever we call "GetCategoryByID" with any string,
            // it should return the specified return value
            _categoriesRepositoryMockFactory.Setup(temp => temp.GetCategoryByID(It.IsAny<Guid>())).ReturnsAsync(category);

            //Act
            ProductResponse productResponse = await _productsAdderService.AddProduct(productAddRequest);

            //Assert
            productResponse.ProductId.Should().Be(product.ToProductResponse().ProductId);
        }

    }
}

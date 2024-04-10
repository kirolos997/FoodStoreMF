using FluentAssertions;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.Services.Products.v1;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using Moq;

namespace FoodStore.ServiceTests.Products.v1
{
    public class ProductsDeleterTests
    {
        private readonly IProductsDeleterService _productsDeleterService;

        private readonly Mock<IProductsRepository> _productsRepositoryMockFactory;

        private readonly IProductsRepository _productsRepository;

        public ProductsDeleterTests()
        {
            // Mocking what the ProductDeleterService depends on
            _productsRepositoryMockFactory = new Mock<IProductsRepository>();

            // Getting the mocked object
            _productsRepository = _productsRepositoryMockFactory.Object;

            // XUnit does not provide the concept of dependency injection.Hence, create the object 
            _productsDeleterService = new ProductsDeleterService(_productsRepository);
        }

        //If you supply an valid PersonID, it should return true
        [Fact]
        public async Task DeletePerson_ValidPersonID_ToBeSuccessful()
        {
            //Arrange
            Product product = new Product()
            {
                ProductName = "p1",
                CategoryId = Guid.NewGuid(),
                InStore = true,
                Price = 10,
                ProductDescription = "",
                ProductId = Guid.NewGuid(),
                Category = new Category() { }
            };


            _productsRepositoryMockFactory.Setup(temp => temp.DeleteProductByID(It.IsAny<Guid>())).ReturnsAsync(true);

            _productsRepositoryMockFactory.Setup(temp => temp.GetProductByID(It.IsAny<Guid>())).ReturnsAsync(product);

            //Act
            bool isDeleted = await _productsDeleterService.DeleteProduct(product.ProductId);

            //Assert
            isDeleted.Should().BeTrue();
        }


        [Fact]
        public async Task DeleteProduct_InvalidProductID_ToBeInvalidProductIDException()
        {
            //Act
            Func<Task> action = async () =>
            {
                await _productsDeleterService.DeleteProduct(Guid.NewGuid());
            };

            //Assert
            await action.Should().ThrowAsync<InvalidProductIDException>();
        }

        [Fact]
        public async Task DeleteProduct_NullProductID_ToBeArgumentNullException()
        {
            //Act
            Func<Task> action = async () =>
            {
                await _productsDeleterService.DeleteProduct(null);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}

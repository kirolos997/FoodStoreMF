using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.IProducts.v1;

namespace FoodStore.Core.Services.Products.v1
{
    public class ProductsGetterService : IProductsGetterService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsGetterService(IProductsRepository productsRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _productsRepository = productsRepository;
        }

        public async Task<List<ProductResponse>> GetAllProducts(Pagination pagination)
        {
            // Getting all products from the data store
            List<Product> products = await _productsRepository.GetAllProducts(pagination);

            return products.Select(item => item.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse?> GetProductByProductID(Guid? productID)
        {
            // Making sure that the given ID is not null
            if (productID is null)
            {
                throw new ArgumentNullException(nameof(productID));
            }
            // Making sure the given id exists inside the data store
            Product? product = await _productsRepository.GetProductByID(productID.Value) ?? throw new InvalidCategoryIDException("Given product id doesn't exist");

            // Performing the deletion operation using the given ID
            return product.ToProductResponse();
        }
    }
}

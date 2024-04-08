using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.IProducts.v1;

namespace FoodStore.Core.Services.Products.v1
{

    public class ProductsDeleterService : IProductsDeleterService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsDeleterService(IProductsRepository productsRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _productsRepository = productsRepository;
        }

        public async Task<bool> DeleteProduct(Guid? productID)
        {
            // Making sure that the passed ID is not null
            if (productID is null)
            {
                throw new ArgumentNullException(nameof(productID));
            }
            // Making sure the given id exists inside the data store before deletion operation
            _ = await _productsRepository.GetProductByID(productID.Value) ?? throw new InvalidProductIDException("Given product id doesn't exist");

            // Performing the deletion operation using the given ID
            return await _productsRepository.DeleteProductByID(productID.Value);
        }
    }
}



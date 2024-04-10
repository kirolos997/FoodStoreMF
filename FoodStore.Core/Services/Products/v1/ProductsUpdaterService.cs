using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using FoodStore.Core.ServicesContracts.IProducts.v1;

namespace FoodStore.Core.Services.Products.v1
{
    /// <summary>
    ///  Product service layer (Updater) where business logic lives and it calls the repository layer
    /// </summary>
    public class ProductsUpdaterService : IProductsUpdaterService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICategoriesGetterService _categoriesGetterService;

        public ProductsUpdaterService(IProductsRepository productRepository, ICategoriesGetterService categoriesGetterService)
        {
            // Using dependency injection to reach the needed repositroy
            _productRepository = productRepository;
            _categoriesGetterService = categoriesGetterService;
        }
        public async Task<ProductResponse> UpdateProduct(Guid? productID, ProductUpdateRequest productUpdateRequest)
        {
            // Making sure that the passed ID is not null
            if (productID is null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            if (productUpdateRequest is null)
            {

                throw new ArgumentNullException(nameof(productID));
            }

            // Converting ProductUpdateRequest to product object
            Product product = productUpdateRequest.ToProduct();

            // Setting productID
            product.ProductId = productID.Value;

            // Making sure the passed CategoryID exists before updating the record. 
            _ = await _categoriesGetterService.GetCategoryByCategoryID(product.CategoryId);

            // Getting the product object we need to update
            Product? updatedProduct = await _productRepository.UpdateProduct(productID, product) ?? throw new InvalidProductIDException("Given product id doesn't exist");

            // Returning the modified product as ProductResponse
            return updatedProduct.ToProductResponse();
        }
    }
}

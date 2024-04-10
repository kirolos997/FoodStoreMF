using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Products;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using FoodStore.Core.ServicesContracts.IProducts;

namespace FoodStore.Core.Services.Products
{
    public class ProductsAdderService : IProductsAdderService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesGetterService _categoriesGetterService;
        public ProductsAdderService(IProductsRepository productsRepository, ICategoriesGetterService categoriesGetterService)
        {
            // Using dependency injection to reach the needed repositroy
            _productsRepository = productsRepository;
            _categoriesGetterService = categoriesGetterService;

        }
        public async Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest)
        {
            if (productAddRequest is null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }
            // Converting productAddRequest to product
            Product product = productAddRequest.ToProduct();

            // Creating new Guid that acts as PK
            product.ProductId = Guid.NewGuid();

            // Making sure no product with the same name exists before
            Product? createdProduct = await _productsRepository.GetProductByName(product.ProductName);

            if (createdProduct != null) throw new DuplicateProductException("Given product name exists before!");

            // Making sure the passed CategoryID exists before creating the record. 
            _ = await _categoriesGetterService.GetCategoryByCategoryID(product.CategoryId);

            // Adding product object to the data store
            Product addedProduct = await _productsRepository.AddProduct(product);

            // Returning the addedProduct as ProductResponse
            return addedProduct.ToProductResponse();


        }
    }
}

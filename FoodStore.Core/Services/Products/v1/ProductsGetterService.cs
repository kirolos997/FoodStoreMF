using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.DTO.QueryFilters;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.Helpers;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using System.Linq.Expressions;

namespace FoodStore.Core.Services.Products.v1
{
    /// <summary>
    ///  Product service layer (Getter) where business logic lives and it calls the repository layer
    /// </summary>
    public class ProductsGetterService : IProductsGetterService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsGetterService(IProductsRepository productsRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _productsRepository = productsRepository;
        }

        public async Task<List<ProductResponse>> GetAllProducts(Pagination pagination, FilterOptions<ProductResponse>? searchOptions)
        {
            // Getting all products from the data store
            List<FilterTerm>? validSearchTerms = null;

            Expression<Func<Product, bool>>? searchExpression = null;

            if (searchOptions is not null)
            {
                validSearchTerms = searchOptions.GetValidTerms().ToList();

                searchExpression = LINQExpressionsBuilder.GetAndFilterExpression<Product>(validSearchTerms);
            }
            List<Product> products = await _productsRepository.GetAllProducts(pagination, searchExpression);

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

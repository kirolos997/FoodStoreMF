using FoodStore.Core.DTO.Products.v1;

namespace FoodStore.Core.ServicesContracts.IProducts
{
    /// <summary>
    /// Represents business logic (insert) for manipulating product entity
    /// </summary
    public interface IProductsAdderService
    {    /// <summary>
         /// Adds a product object to the products table
         /// </summary>
         /// <param name="productAddRequest">product object to add</param>
         /// <returns>Returns the product response object after adding it (including newly generated product id)</returns>
        public Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest);
    }
}

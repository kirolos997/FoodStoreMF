using FoodStore.Core.DTO.Products.v1;

namespace FoodStore.Core.ServicesContracts.IProducts.v1
{
    /// <summary>   
    /// Represents business logic (update) for manipulating product entity 
    /// </summary>
    public interface IProductsUpdaterService
    {
        /// <summary>
        /// Updates the specified product details based on the given product ID
        /// </summary>
        /// <param name="productID">Product id to search</param>
        /// <param name="productUpdateRequest">Product details to update, including product id</param>
        /// <returns>Returns the product response object after updating it</returns>
        Task<ProductResponse> UpdateProduct(Guid? productID, ProductUpdateRequest productUpdateRequest);
    }
}

using FoodStore.Application.DTO.Products;

namespace FoodStore.Core.ServicesContracts.IProducts.v1
{
    public interface IProductsGetterService
    {
        /// <summary>
        /// Returns all products as list
        /// </summary>
        /// <returns>Returns a list of objects of ProductResponse type</returns>
        Task<List<ProductResponse>> GetAllProducts();

        /// <summary>
        /// Returns the product object based on the given product id
        /// </summary>
        /// <param name="productID">Product id to search</param>
        /// <returns>Returns matching product object</returns>
        Task<ProductResponse?> GetProductByProductID(Guid? productID);
    }
}

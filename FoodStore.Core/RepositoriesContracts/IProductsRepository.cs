using FoodStore.Core.Entities;

namespace FoodStore.Core.RepositoriesContracts
{
    /// <summary>
    /// Represents data access logic for managing Products entity (DAL)
    /// </summary>
    public interface IProductsRepository
    {

        /// <summary>
        /// Returns all products in the data store
        /// </summary>
        /// <returns>List of product objects from table</returns>
        Task<List<Product>> GetAllProducts();


        /// <summary>
        /// Returns a product object based on the given id
        /// </summary>
        /// <param name="productID">productID (guid) to search</param>
        /// <returns>A product object or null</returns>
        Task<Product?> GetProductByID(Guid productID);


        /// <summary>
        /// Deletes a product object based on the id
        /// </summary>
        /// <param name="productID">Product ID (guid) to search</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        Task<bool> DeleteProductByID(Guid productID);


        /// <summary>
        /// Updates a product object based on the given id
        /// </summary>
        /// <param name="product">Product object to update</param>
        /// <param name="productID">productID (guid) to search</param>
        /// <returns>Returns the updated product object</returns>
        Task<Product?> UpdateProduct(Guid? productID, Product product);
    }
}

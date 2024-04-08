using FoodStore.Core.DTO.Pagination;
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
        /// <param name="pagination">Optional pagination object</param>
        /// <returns>List of product objects from table</returns>
        Task<List<Product>> GetAllProducts(Pagination pagination);


        /// <summary>
        /// Returns a product object based on the given id
        /// </summary>
        /// <param name="productID">productID (guid) to search</param>
        /// <returns>A product object or null</returns>
        Task<Product?> GetProductByID(Guid productID);

        /// <summary>
        /// Returns a product object based on the given productName; otherwise, it returns null
        /// </summary>
        /// <param name="productName">productName to search</param>
        /// <returns>Matching product or null</returns>
        Task<Product?> GetProductByName(string productName);

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

        /// <summary>
        /// Adds a product object to the data store
        /// </summary>
        /// <param name="product">product object to add</param>
        /// <returns>Returns the product object after adding it to the table</returns>
        Task<Product> AddProduct(Product product);
    }
}

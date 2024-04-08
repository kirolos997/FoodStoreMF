using FoodStore.Core.Entities;

namespace FoodStore.Core.RepositoriesContracts
{
    /// <summary>
    /// Represents data access logic for managing category entity
    /// </summary>
    public interface ICategoriesRepository
    {
        /// <summary>
        /// Returns all categories in the data store
        /// </summary>
        /// <returns>All categories from the table</returns>
        Task<List<Category>> GetAllCategories();

        /// <summary>
        /// Returns a category object based on the given id; otherwise, it returns null
        /// </summary>
        /// <param name="ID">ID to search</param>
        /// <returns>Matching category or null</returns>
        Task<Category?> GetCategoryByID(Guid ID);

        /// <summary>
        /// Returns a category object based on the given categoryName; otherwise, it returns null
        /// </summary>
        /// <param name="categoryName">categoryName to search</param>
        /// <returns>Matching category or null</returns>
        Task<Category?> GetCategoryByName(string categoryName);

        /// <summary>
        /// Deletes a category object based on the id
        /// </summary>
        /// <param name="categoryID">Category ID (guid) to search</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        Task<bool> DeleteCategoryByID(Guid categoryID);

        /// <summary>
        /// Updates a category object based on the given id
        /// </summary>
        /// <param name="category">Category object to update</param>
        /// <returns>Returns the updated category object</returns>
        Task<Category?> UpdateCategory(Guid? categoryID, Category category);


        /// <summary>
        /// Adds a new category object to the data store
        /// </summary>
        /// <param name="category">Category object to add</param>
        /// <returns>Returns the category object after adding it to the data store</returns>
        Task<Category> AddCategory(Category category);


    }
}

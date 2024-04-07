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

    }
}

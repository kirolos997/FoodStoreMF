using FoodStore.Core.DTO.Categories.v1;
using FoodStore.Core.DTO.Pagination;

namespace FoodStore.Core.ServicesContracts.ICategories.v1
{
    /// <summary>
    /// Represents business logic (retrieve) for manipulating Category entity
    /// </summary>
    public interface ICategoriesGetterService
    {
        /// <summary>
        /// Returns all categories from the table
        /// <param name="pagination">Optional pagination object</param>
        /// </summary>
        /// <returns>All categories from the table as List of CategoryResponse</returns>
        Task<List<CategoryResponse>> GetAllCategories(Pagination pagination);


        /// <summary>
        /// Returns a category object based on the given categoryResponse ID
        /// </summary>
        /// <param name="categoryID">CategoryID (guid) to search</param>
        /// <returns>Matching category as CategoryResponse object</returns>
        Task<CategoryResponse?> GetCategoryByCategoryID(Guid? categoryID);
    }
}

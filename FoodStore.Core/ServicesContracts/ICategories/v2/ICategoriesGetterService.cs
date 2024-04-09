using FoodStore.Application.DTO.Categories.v2;
using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.QueryFilters;

namespace FoodStore.Core.ServicesContracts.ICategories.v2
{
    /// <summary>
    /// Represents business logic (retrieve) for manipulating Category entity
    /// </summary>
    public interface ICategoriesGetterService
    {
        /// <summary>
        /// Returns all categories from the table
        /// </summary>
        /// <param name="pagination">Optional pagination object</param>
        /// <param name="searchOptions">Optional searchOptions object to apply filtering</param>
        /// <returns>All categories from the table as List of CategoryResponse</returns>
        Task<List<CategoryResponse>> GetAllCategories(Pagination pagination, FilterOptions<CategoryResponse>? searchOptions);


        /// <summary>
        /// Returns a category object based on the given categoryResponse ID
        /// </summary>
        /// <param name="categoryID">CategoryID (guid) to search</param>
        /// <returns>Matching category as CategoryResponse object</returns>
        Task<CategoryResponse?> GetCategoryByCategoryID(Guid? categoryID);
    }
}

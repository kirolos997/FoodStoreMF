using FoodStore.Core.DTO.Categories.v1;

namespace FoodStore.Core.ServicesContracts.ICategories.v1
{
    /// <summary>
    /// Represents business logic (update) for manipulating category entity
    /// </summary>
    public interface ICategoriesUpdaterService
    {
        /// <summary>
        /// Updates the specified category details based on the given category ID
        /// </summary>
        /// <param name="categoryUpdateRequest">Category details to update, including category id</param>
        /// <returns>Returns the category response object after updating it</returns>
        Task<CategoryResponse> UpdateCategory(Guid? categoryID, CategoryUpdateRequest categoryUpdateRequest);
    }
}

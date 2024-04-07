using FoodStore.Core.Entities;

namespace FoodStore.Application.DTO.Categories
{
    /// <summary>
    /// DTO for getting a category response object
    /// </summary>
    public class CategoryResponse
    {
        public Guid CategoryID { get; set; }

        public string? CategoryName { get; set; }

        /// <summary>
        /// Converts the current object of CategoryAddRequest into a new object of Category type
        /// </summary>
        /// <returns></returns>
        public Category ToCategory()
        {
            return new Category() { Name = CategoryName };
        }
    }
    public static class CategoryExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Category class into CategoryResponse class
        /// </summary>
        /// <param name="category">The category object to convert</param>
        /// <returns>Returns the converted CategoryResponse object</returns>
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse()
            {
                CategoryID = category.CategoryId,
                CategoryName = category.Name,

            };
        }
    }
}

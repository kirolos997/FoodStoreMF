using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Helpers;

namespace FoodStore.Application.DTO.Categories.v2
{
    /// <summary>
    /// DTO for getting a category response object
    /// </summary>
    public class CategoryResponse
    {
        public Guid CategoryID { get; set; }

        [Searchable]
        public string? Name { get; set; }

        public List<ProductResponse>? Products { get; set; }


        /// <summary>
        /// Converts the current object of CategoryAddRequest into a new object of Category type
        /// </summary>
        /// <returns></returns>
        public Category ToCategory()
        {
            return new Category() { Name = Name };
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
                Name = category.Name,
                Products = category.products?.Select(p => p.ToProductResponse()).ToList()

            };
        }
    }
}

using FoodStore.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.DTO.Categories.v1
{
    /// <summary>
    /// DTO for adding new category request object
    /// </summary>
    public class CategoryAddRequest
    {
        [Required(ErrorMessage = "Category name can't be blank or missing")]
        [StringLength(50, ErrorMessage = "Category name can't be more than 50 characters")]
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
}

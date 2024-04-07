using FoodStore.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Application.DTO.Categories
{
    /// <summary>
    /// DTO for updating a category request object
    /// </summary>
    public class CategoryUpdateRequest
    {
        [Required(ErrorMessage = "Category ID can't be blank")]
        public Guid CategoryID { get; set; }

        [Required(ErrorMessage = "Category name can't be blank")]
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

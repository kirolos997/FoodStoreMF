using FoodStore.Core.Entities;
using FoodStore.Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.DTO.Products.v1
{
    /// <summary>
    /// DTO for updating new product request object
    /// </summary>
    public class ProductUpdateRequest
    {
        [Required(ErrorMessage = "Product ID can't be blank")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product name can't be blank")]
        [StringLength(50, ErrorMessage = "Product name can't be more than 50 characters")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product price can't be blank")]
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Price must be a decimal with up to 2 decimal places.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product description can't be blank")]
        [StringLength(150, ErrorMessage = "Product description can't be more than 150 characters")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Category Id can't be blank")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "InStore can't be blank")]
        [BooleanValidator]
        public bool InStore { get; set; }

        /// <summary>
        /// Converts the current object of ProductUpdateRequest into a new object of Product type
        /// </summary>
        /// <returns></returns>
        public Product ToProduct()
        {
            return new Product()
            {
                ProductId = ProductId,
                ProductName = ProductName,
                Price = Price,
                ProductDescription = ProductDescription,
                CategoryId = CategoryId,
                InStore = InStore
            };
        }
    }
}

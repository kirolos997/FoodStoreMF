using FoodStore.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.DTO.Products.v1
{
    /// <summary>
    /// DTO for adding new product request object
    /// </summary>
    public class ProductAddRequest
    {
        [Required(ErrorMessage = "Product name can't be blank")]
        [StringLength(50, ErrorMessage = "Product name can't be more than 50 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product price can't be blank")]
        [RegularExpression(@"^\d+(\.\d{2})?$", ErrorMessage = "Price must be a decimal with up to 2 decimal places.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product description can't be blank")]
        [StringLength(150, ErrorMessage = "Product description can't be more than 150 characters")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Category Id can't be blank")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "InStore can't be blank")]
        [RegularExpression("^(True|False|true|false)$", ErrorMessage = "Invalid boolean value for the InStore")]
        public bool InStore { get; set; }


        /// <summary>
        /// Converts the current object of ProductAddRequest into a new object of Product type
        /// </summary>
        /// <returns></returns>

        public Product ToProduct()
        {
            return new Product()
            {
                ProductName = ProductName,
                Price = Price,
                ProductDescription = ProductDescription,
                CategoryId = CategoryId,
                InStore = InStore
            };
        }

    }

}

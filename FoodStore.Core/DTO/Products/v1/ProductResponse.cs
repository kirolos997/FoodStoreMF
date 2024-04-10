using FoodStore.Core.Entities;
using FoodStore.Core.Helpers;

namespace FoodStore.Core.DTO.Products.v1
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }

        [Searchable]
        public string ProductName { get; set; }
        [Searchable]
        public decimal Price { get; set; }
        [Searchable]
        public bool InStore { get; set; }
        public string? ProductDescription { get; set; }
        public Guid? CategoryId { get; set; }

    }

    public static class ProductExtension
    {
        /// <summary>
        /// An extension method to convert an object of Product class into ProductResponse class
        /// </summary>
        /// <param name="product">The product object to convert</param>
        /// <returns>Returns the converted ProductResponse object</returns>
        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductDescription = product.ProductDescription,
                InStore = product.InStore,
                CategoryId = product.CategoryId
            };
        }
    }
}

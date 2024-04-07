using FoodStore.Core.Entities;

namespace FoodStore.Application.DTO.Products
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string? ProductDescription { get; set; }

        public Guid? CategoryId { get; set; }

        public bool InStore { get; set; }
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

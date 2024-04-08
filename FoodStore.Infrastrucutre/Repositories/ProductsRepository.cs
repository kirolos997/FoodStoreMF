using FoodStore.Core.Entities;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FoodStore.Infrastrucutre.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly FoodStoreDbContext _db;

        public ProductsRepository(FoodStoreDbContext db)
        {
            // Dependency injection for the DbContext object from the IOC container
            _db = db;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            // Using navigation property
            return await _db.products.Include("Category").ToListAsync();
        }


        public async Task<Product?> GetProductByID(Guid productID)
        {
            // Using navigation property
            return await _db.products.Include("Category").FirstOrDefaultAsync(item => item.ProductId == productID);
        }

        public async Task<bool> DeleteProductByID(Guid productID)
        {
            _db.products.RemoveRange(_db.products.Where(item => item.ProductId == productID));

            int rowDeleted = await _db.SaveChangesAsync();

            return rowDeleted > 0;
        }

        public async Task<Product?> UpdateProduct(Guid? id, Product product)
        {
            Product? retrievedItem = await _db.products.FirstOrDefaultAsync(item => item.ProductId == id);

            if (retrievedItem == null)
                return null;

            // Using reflection to copy properties of one object to another
            PropertyInfo[] properties = typeof(Product).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite && property.Name != "ProductId")
                {
                    var value = property.GetValue(product);

                    property.SetValue(retrievedItem, value);
                }
            }

            await _db.SaveChangesAsync();

            return retrievedItem;
        }

    }
}

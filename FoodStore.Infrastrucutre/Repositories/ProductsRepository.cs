using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.Entities;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FoodStore.Infrastrucutre.Repositories
{
    /// <summary>
    /// Following repository pattern to create a layer for accessing the database
    /// </summary>
    public class ProductsRepository : IProductsRepository
    {
        private readonly FoodStoreDbContext _db;

        public ProductsRepository(FoodStoreDbContext db)
        {
            // Dependency injection for the DbContext object from the IOC container
            _db = db;
        }

        public async Task<List<Product>> GetAllProducts(Pagination pagination, Expression<Func<Product, bool>>? searchOptions)
        {
            if (searchOptions is null)
            {
                // No filter is applied
                return await _db.products.Include("Category").Skip(pagination.Offset).Take(pagination.Limit).ToListAsync();

            }
            return await _db.products.Include("Category").Where(searchOptions).Skip(pagination.Offset).Take(pagination.Limit).ToListAsync();

        }

        public async Task<Product?> GetProductByName(string productName)
        {
            string modifiedProductName = productName.ToLower().Trim().Replace(" ", "");

            return await _db.products.Include("Category").FirstOrDefaultAsync(item => item.ProductName.ToLower().Trim().Replace(" ", "").Contains(modifiedProductName));
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

        public async Task<Product> AddProduct(Product product)
        {
            _db.products.Add(product);

            await _db.SaveChangesAsync();

            return product;
        }

    }
}

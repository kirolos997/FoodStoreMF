using FoodStore.Core.Entities;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;

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

    }
}

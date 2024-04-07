using FoodStore.Core.Entities;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Infrastrucutre.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Infrastrucutre.Repositories
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly FoodStoreDbContext _db;

        public CategoryRepository(FoodStoreDbContext db)
        {
            // Dependency injection for the DbContext object from the IOC container
            _db = db;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _db.categories.Include("products").ToListAsync();
        }

        public async Task<Category?> GetCategoryByID(Guid ID)
        {
            return await _db.categories.Include("products").FirstOrDefaultAsync(item => item.CategoryId == ID);
        }


    }
}

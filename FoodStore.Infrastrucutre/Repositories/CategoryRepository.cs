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

        public async Task<bool> DeleteCategoryByID(Guid categoryID)
        {
            _db.categories.RemoveRange(_db.categories.Where(item => item.CategoryId == categoryID));

            int rowDeleted = await _db.SaveChangesAsync();

            return rowDeleted > 0;
        }
        public async Task<Category?> UpdateCategory(Guid? id, Category category)
        {
            Category? retrievedItem = await _db.categories.Include("products").FirstOrDefaultAsync(item => item.CategoryId == id);

            if (retrievedItem == null)
                return null;

            retrievedItem.Name = category.Name;

            await _db.SaveChangesAsync();

            return retrievedItem;
        }
    }
}

using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories.v1;

namespace FoodStore.Core.Services.Categories.v1
{
    public class CategoriesDeleterService : ICategoriesDeleterService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesDeleterService(ICategoriesRepository categoriesRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _categoriesRepository = categoriesRepository;
        }
        public async Task<bool> DeleteCategory(Guid? categoryID)
        {
            // Making sure that the passed ID is not null
            if (categoryID is null)
            {
                throw new ArgumentNullException(nameof(categoryID));
            }
            // Making sure the given id exists inside the data store before deletion operation
            _ = await _categoriesRepository.GetCategoryByID(categoryID.Value) ?? throw new InvalidCategoryIDException("Given category id doesn't exist");

            // Performing the deletion operation using the given ID
            return await _categoriesRepository.DeleteCategoryByID(categoryID.Value);
        }
    }
}

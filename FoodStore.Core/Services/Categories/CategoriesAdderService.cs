using FoodStore.Application.DTO.Categories;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories;

namespace FoodStore.Core.Services.Categories
{
    public class CategoriesAdderService : ICategoriesAdderService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesAdderService(ICategoriesRepository categoriesRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _categoriesRepository = categoriesRepository;

        }

        public async Task<CategoryResponse> AddCategory(CategoryAddRequest categoryAddRequest)
        {

            // Converting categoryAddRequest to category
            Category category = categoryAddRequest.ToCategory();

            // Creating new Guid that acts as PK
            category.CategoryId = Guid.NewGuid();

            // Making sure no category with the same name exists before
            Category? createdCategory = await _categoriesRepository.GetCategoryByName(category.Name);

            if (createdCategory != null) throw new DuplicateCategoryException("Given category name exists before!");

            // Adding category object to the data store
            Category addedCategory = await _categoriesRepository.AddCategory(category);

            // Returning the category as CategoryResponse
            return addedCategory.ToCategoryResponse();

        }
    }
}

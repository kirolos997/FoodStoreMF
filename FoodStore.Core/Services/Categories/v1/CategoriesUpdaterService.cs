﻿using FoodStore.Core.DTO.Categories.v1;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories.v1;

namespace FoodStore.Core.Services.Categories.v1
{
    /// <summary>
    ///  Category service layer (Updater) where business logic lives and it calls the repository layer
    /// </summary>
    public class CategoriesUpdaterService : ICategoriesUpdaterService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesUpdaterService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<CategoryResponse> UpdateCategory(Guid? categoryID, CategoryUpdateRequest categoryUpdateRequest)
        {
            // Making sure that the passed ID is not null
            if (categoryID is null)
            {
                throw new ArgumentNullException(nameof(categoryID));
            }
            // Converting categoryAddRequest to category object
            Category category = categoryUpdateRequest.ToCategory();

            // Getting the category object we need to update
            Category? updatedCategory = await _categoriesRepository.UpdateCategory(categoryID, category) ?? throw new InvalidCategoryIDException("Given category id doesn't exist");

            // Returning the modified category as CategoryResponse
            return updatedCategory.ToCategoryResponse();

        }
    }
}

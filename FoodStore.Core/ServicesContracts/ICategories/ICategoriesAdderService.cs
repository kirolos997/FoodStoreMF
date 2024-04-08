﻿using FoodStore.Application.DTO.Categories;

namespace FoodStore.Core.ServicesContracts.ICategories
{
    /// <summary>
    /// Represents business logic (insert) for manipulating Category entity
    /// </summary>
    public interface ICategoriesAdderService
    {
        /// <summary>
        /// Adds a category object to the categories table
        /// </summary>
        /// <param name="categoryAddRequest">Category object to add</param>
        /// <returns>Returns the category response object after adding it (including newly generated category id)</returns>
        public Task<CategoryResponse> AddCategory(CategoryAddRequest categoryAddRequest);
    }
}

﻿using FoodStore.Application.DTO.Categories;

namespace FoodStore.Core.ServicesContracts.ICategories
{
    /// <summary>
    /// Represents business logic (retrieve) for manipulating Category entity
    /// </summary>
    public interface ICategoriesGetterService
    {
        /// <summary>
        /// Returns all categories from the table
        /// </summary>
        /// <returns>All categories from the table as List of CategoryResponse</returns>
        Task<List<CategoryResponse>> GetAllCategories();


        /// <summary>
        /// Returns a category object based on the given categoryResponse ID
        /// </summary>
        /// <param name="categoryID">CategoryID (guid) to search</param>
        /// <returns>Matching category as CategoryResponse object</returns>
        Task<CategoryResponse?> GetCategoryByCategoryID(Guid? categoryID);
    }
}

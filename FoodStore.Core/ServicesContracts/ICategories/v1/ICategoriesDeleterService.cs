﻿namespace FoodStore.Core.ServicesContracts.ICategories.v1
{
    /// <summary>
    /// Represents business logic (delete) for manipulating category entity
    /// </summary>
    public interface ICategoriesDeleterService
    {
        /// <summary>
        /// Deletes a category based on the given category id
        /// </summary>
        /// <param name="categoryID">Category ID to be deleted</param>
        /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        public Task<bool> DeleteCategory(Guid? categoryID);
    }
}

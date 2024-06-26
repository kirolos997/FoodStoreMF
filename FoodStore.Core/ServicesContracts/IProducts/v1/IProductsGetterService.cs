﻿using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.DTO.QueryFilters;

namespace FoodStore.Core.ServicesContracts.IProducts.v1
{
    public interface IProductsGetterService
    {
        /// <summary>
        /// Returns all products as list
        /// </summary>
        /// <param name="pagination">Optional pagination object</param>
        /// <param name="searchOptions">Optional searchOptions object to apply filtering</param>
        /// <returns>Returns a list of objects of ProductResponse type</returns>
        Task<List<ProductResponse>> GetAllProducts(Pagination pagination, FilterOptions<ProductResponse>? searchOptions);

        /// <summary>
        /// Returns the product object based on the given product id
        /// </summary>
        /// <param name="productID">Product id to search</param>
        /// <returns>Returns matching product object</returns>
        Task<ProductResponse?> GetProductByProductID(Guid? productID);
    }
}

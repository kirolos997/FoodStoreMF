using FoodStore.Application.DTO.Categories.v2;
using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.QueryFilters;
using FoodStore.Core.Entities;
using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.Helpers;
using FoodStore.Core.RepositoriesContracts;
using FoodStore.Core.ServicesContracts.ICategories.v2;
using System.Linq.Expressions;

namespace FoodStore.Core.Services.Categories.v2
{
    /// <summary>
    ///  Category service layer (Getter) where business logic lives and it calls the repository layer
    /// </summary>
    public class CategoriesGetterService : ICategoriesGetterService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesGetterService(ICategoriesRepository categoriesRepository)
        {
            // Using dependency injection to reach the needed repositroy
            _categoriesRepository = categoriesRepository;
        }
        public async Task<List<CategoryResponse>> GetAllCategories(Pagination pagination, FilterOptions<CategoryResponse>? searchOptions)
        {
            // Getting all categories from the data store
            List<FilterTerm>? validSearchTerms = null;

            Expression<Func<Category, bool>>? searchExpression = null;

            if (searchOptions is not null)
            {
                validSearchTerms = searchOptions.GetValidTerms().ToList();

                searchExpression = LINQExpressionsBuilder.GetAndFilterExpression<Category>(validSearchTerms);
            }
            List<Category> categories = await _categoriesRepository.GetAllCategories(pagination, searchExpression);

            // Converting each Category to CategoryResponse using LINQ
            return categories.Select(item => item.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse?> GetCategoryByCategoryID(Guid? categoryID)
        {
            // Making sure that the passed ID is not null
            if (categoryID is null)
            {
                throw new ArgumentNullException(nameof(categoryID));
            }
            // Making sure the given id exists inside the data store
            Category? category = await _categoriesRepository.GetCategoryByID(categoryID.Value) ?? throw new InvalidCategoryIDException("Given CategoryID doesn't exist or not formatted correctly");

            // Performing the deletion operation using the given ID
            return category.ToCategoryResponse();
        }


    }
}

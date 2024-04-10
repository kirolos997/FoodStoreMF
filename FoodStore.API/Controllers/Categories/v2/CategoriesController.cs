using Asp.Versioning;
using FoodStore.Core.DTO.Categories.v2;
using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.DTO.QueryFilters;
using FoodStore.Core.ServicesContracts.ICategories.v2;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.API.Controllers.Categories.v2
{
    [ApiVersion("2")]

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesGetterService _categoriesGetterService;

        public CategoriesController(ICategoriesGetterService categoriesGetterService)
        {
            // Using dependency injection to reach the needed service
            _categoriesGetterService = categoriesGetterService;
        }

        // GET: api/Categories/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination, [FromQuery] FilterOptions<CategoryResponse>? searchOptions)
        {
            List<CategoryResponse>? respone = await _categoriesGetterService.GetAllCategories(pagination, searchOptions);

            return Ok(respone);
        }

        // GET api/Categories/GUID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            CategoryResponse? respone = await _categoriesGetterService.GetCategoryByCategoryID(id);

            return Ok(respone);
        }

    }
}

using Asp.Versioning;
using FoodStore.Application.DTO.Categories.v2;
using FoodStore.Core.DTO.Pagination;
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
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            List<CategoryResponse>? respone = await _categoriesGetterService.GetAllCategories(pagination);

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

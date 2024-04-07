using FoodStore.API.Filters;
using FoodStore.Application.DTO.Categories;
using FoodStore.Core.ServicesContracts.ICategories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;
        public CategoriesController(ICategoriesGetterService categoriesGetterService, ICategoriesUpdaterService categoriesUpdaterService)
        {
            // Using dependency injection to reach the needed service
            _categoriesGetterService = categoriesGetterService;
            _categoriesUpdaterService = categoriesUpdaterService;
        }

        // GET: api/Categories/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CategoryResponse>? respone = await _categoriesGetterService.GetAllCategories();

            return Ok(respone);
        }

        // GET api/Categories/GUID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            CategoryResponse? respone = await _categoriesGetterService.GetCategoryByCategoryID(id);

            return Ok(respone);
        }

        // PUT api/Categories/GUID
        [HttpPut("{categoryID}")]
        [TypeFilter(typeof(ValidateModelAttributes))]
        public async Task<IActionResult> Put([FromRoute] Guid categoryID, [FromBody] CategoryUpdateRequest categoryUpdateRequest)
        {
            CategoryResponse? respone = await _categoriesUpdaterService.UpdateCategory(categoryID, categoryUpdateRequest);

            return Ok(respone);
        }

        // DELETE api/Categories/GUID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}

using Asp.Versioning;
using FoodStore.Core.DTO.Categories.v1;
using FoodStore.Core.DTO.Pagination;
using FoodStore.Core.ServicesContracts.ICategories.v1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.API.Controllers.Categories.v1
{

    [ApiVersion("1")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;
        private readonly ICategoriesDeleterService _categoriesDeleterService;
        private readonly ICategoriesAdderService _categoriesAdderService;

        public CategoriesController(ICategoriesGetterService categoriesGetterService,
            ICategoriesUpdaterService categoriesUpdaterService,
            ICategoriesDeleterService categoriesDeleterService,
            ICategoriesAdderService categoriesAdderService)
        {
            // Using dependency injection to reach the needed service
            _categoriesGetterService = categoriesGetterService;
            _categoriesUpdaterService = categoriesUpdaterService;
            _categoriesDeleterService = categoriesDeleterService;
            _categoriesAdderService = categoriesAdderService;
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

        // PUT api/Categories/GUID
        [HttpPut("{categoryID}")]
        public async Task<IActionResult> Put([FromRoute] Guid categoryID, [FromBody] CategoryUpdateRequest categoryUpdateRequest)
        {
            CategoryResponse? respone = await _categoriesUpdaterService.UpdateCategory(categoryID, categoryUpdateRequest);

            return Ok(respone);
        }

        // DELETE api/Categories/GUID
        [HttpDelete("{categoryID}")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryID)
        {
            _ = await _categoriesDeleterService.DeleteCategory(categoryID);

            return NoContent();
        }

        // POST api/Categories
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryAddRequest categoryAddRequest)
        {
            CategoryResponse? respone = await _categoriesAdderService.AddCategory(categoryAddRequest);

            return Ok(respone);
        }


    }
}

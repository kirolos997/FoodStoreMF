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
        public CategoriesController(ICategoriesGetterService categoriesGetterService)
        {
            // Using dependency injection to reach the needed service
            _categoriesGetterService = categoriesGetterService;
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
        public async Task<IActionResult> Get(Guid id)
        {
            CategoryResponse? respone = await _categoriesGetterService.GetCategoryByCategoryID(id);

            return Ok(respone);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

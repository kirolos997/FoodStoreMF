using FoodStore.Application.DTO.Products;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.API.Controllers.Products.v1
{
    public class ProductsController : BaseController
    {
        private readonly IProductsGetterService _productsGetterService;

        public ProductsController(IProductsGetterService productsGetterService)
        {
            _productsGetterService = productsGetterService;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProductResponse>? respone = await _productsGetterService.GetAllProducts();

            return Ok(respone);
        }

        // GET api/Products/GUID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            ProductResponse? respone = await _productsGetterService.GetProductByProductID(id);

            return Ok(respone);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using FoodStore.Core.DTO.Products.v1;
using FoodStore.Core.ServicesContracts.IProducts.v1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.API.Controllers.Products.v1
{
    public class ProductsController : BaseController
    {
        private readonly IProductsGetterService _productsGetterService;
        private readonly IProductsUpdaterService _productsUpdaterService;
        private readonly IProductsDeleterService _productsDeleterService;

        public ProductsController(IProductsGetterService productsGetterService, IProductsUpdaterService productsUpdaterService, IProductsDeleterService productsDeleterService)
        {
            _productsGetterService = productsGetterService;
            _productsUpdaterService = productsUpdaterService;
            _productsDeleterService = productsDeleterService;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProductResponse>? respone = await _productsGetterService.GetAllProducts();

            return Ok(respone);
        }

        // GET api/Products/GUID
        [HttpGet("{productID}")]
        public async Task<IActionResult> Get([FromRoute] Guid productID)
        {
            ProductResponse? respone = await _productsGetterService.GetProductByProductID(productID);

            return Ok(respone);
        }

        // PUT api/Products/GUID
        [HttpPut("{productID}")]
        public async Task<IActionResult> Put([FromRoute] Guid productID, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse? respone = await _productsUpdaterService.UpdateProduct(productID, productUpdateRequest);

            return Ok(respone);
        }

        // DELETE api/Products/GUID
        [HttpDelete("{productID}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productID)
        {
            _ = await _productsDeleterService.DeleteProduct(productID);

            return NoContent();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}

using FluentAssertions;
using FoodStore.Core.DTO.Products.v1;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FoodStore.IntegrationTests.Products.v1
{
    public class ProductsControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

        }
        #region Get Products
        [Fact]
        public async Task GetAllProduct_DefaultValues_ToBeAllProducts()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products");

            //Assert
            response.Should().BeSuccessful(); //2xx


        }
        [Fact]
        public async Task GetAllProduct_Pagination_ToBePaginatedProducts()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?offset=5&limit=4");

            //Assert
            response.Should().BeSuccessful(); //2xx

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().HaveCount(4);
        }
        [Fact]
        public async Task GetAllProduct_InvalidPagination_ToBeAllProducts()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?limhit=4");

            //Assert
            response.Should().BeSuccessful(); //2xx

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().HaveCount(16);
        }
        [Fact]
        public async Task GetAllProduct_LargeOffset_ToBeEmptyList()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?offset=4000");

            //Assert
            response.Should().BeSuccessful(); //2xx

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().BeEmpty();
        }
        [Fact]
        public async Task GetAllProduct_ValidFiltering_ToBeEmptyList()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?search=productName eq NOTFOUND");

            //Assert
            response.Should().BeSuccessful(); //2xx

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().HaveCount(0);
        }
        [Fact]
        public async Task GetAllProduct_ValidFiltering_ToBeOneProduct()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?search=productName eq Popcorn");

            //Assert
            response.Should().BeSuccessful(); //2xx

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().HaveCount(1);
        }
        [Fact]
        public async Task GetAllProduct_InvalidOperator_ToBeInternalServerError()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?search=productName eqe Popcorn");

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.InternalServerError);

        }
        [Fact]
        public async Task GetAllProduct_InvalidFilterTerm_ToBeBadRequest()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?search=productsdsdsdsName eq Popcorn");

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }
        [Fact]
        public async Task GetAllProduct_ValidMultipleSearchTerm_ToBeOneProduct()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/v1/products?search=productName eq Popcorn&search=price eq 5");

            //Assert
            response.Should().BeSuccessful();

            string responseBody = await response.Content.ReadAsStringAsync();

            List<ProductResponse> productResponse = JsonConvert.DeserializeObject<List<ProductResponse>>(responseBody);

            productResponse.Should().HaveCount(1);

        }
        #endregion


        #region Delete Product
        [Fact]
        public async Task DeleteProduct_InvalidProductID_ToBeBadRequest()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.DeleteAsync("/api/v1/products/kkkkkdkdkdk");

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }
        [Fact]
        public async Task DeleteProduct_ValidProductID_ToBeDeleteProduct()
        {
            //Arrange
            Guid productid = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928");
            //Act
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/products/{productid}");

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.NoContent);

        }
        #endregion

        #region Update Product
        [Fact]
        public async Task UpdateProduct_InvalidProductID_ToBeBadRequest()
        {
            //Arrange
            var productid = "djdjjdjdj";
            ProductUpdateRequest productAddRequest = new ProductUpdateRequest();

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/products/{productid}", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }
        [Fact]
        public async Task UpdateProduct_ValidProductIDValidCategoryID_ToBeUpdatedProduct()
        {
            //Arrange
            Guid productid = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928");
            ProductUpdateRequest productAddRequest = new ProductUpdateRequest()
            {
                InStore = false,
                CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                Price = decimal.Parse("12.00"),
                ProductDescription = "dTest",
                ProductName = "p1"
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/products/{productid}", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            string responseBody = await response.Content.ReadAsStringAsync();

            ProductResponse productResponse = JsonConvert.DeserializeObject<ProductResponse>(responseBody);

            productResponse.ProductDescription.Should().BeEquivalentTo("dTest");

        }
        [Fact]
        public async Task UpdateProduct_ValidProductIDInValidCategoryID_ToBeNotFoundt()
        {
            //Arrange
            Guid productid = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928");
            ProductUpdateRequest productAddRequest = new ProductUpdateRequest()
            {
                InStore = false,
                CategoryId = Guid.NewGuid(),
                Price = decimal.Parse("12.00"),
                ProductDescription = "dTest",
                ProductName = "p1"
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/products/{productid}", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);


        }
        [Fact]
        public async Task UpdateProduct_InvalidProductAddRequest_ToBeBadRequest()
        {
            //Arrange
            Guid productid = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928");
            ProductUpdateRequest productAddRequest = new ProductUpdateRequest()
            {
                InStore = false,
                CategoryId = Guid.NewGuid(),
                Price = 12,
                ProductDescription = null,
                ProductName = "p1"
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/products/{productid}", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }
        #endregion

        #region Create Product
        [Fact]
        public async Task CreateProduct_InvalidCategoryID_ToBeNotFound()
        {
            //Arrange
            ProductAddRequest productAddRequest = new ProductAddRequest()
            {
                InStore = false,
                CategoryId = Guid.NewGuid(),
                Price = decimal.Parse("12.00"),
                ProductDescription = "dTest",
                ProductName = "p1"
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PostAsync($"/api/v1/products/", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);


        }
        [Fact]
        public async Task CreateProduct_ValidCategoryIDValidProductAddRequest_ToBeCreatedProduct()
        {
            //Arrange
            ProductAddRequest productAddRequest = new ProductAddRequest()
            {
                InStore = false,
                CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                Price = decimal.Parse("12.00"),
                ProductDescription = "dTest",
                ProductName = "p1"
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PostAsync($"/api/v1/products/", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);

            string responseBody = await response.Content.ReadAsStringAsync();

            ProductResponse productResponse = JsonConvert.DeserializeObject<ProductResponse>(responseBody);

            productResponse.ProductDescription.Should().BeEquivalentTo("dTest");

        }
        [Fact]
        public async Task CreateProduct_ValidCategoryIDInvalidProductAddRequest_ToBeBadRequest()
        {
            //Arrange
            ProductAddRequest productAddRequest = new ProductAddRequest()
            {
                InStore = false,
                CategoryId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                Price = decimal.Parse("12.00"),
                ProductDescription = null,
                ProductName = null
            };

            string body = JsonConvert.SerializeObject(productAddRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await _client.PostAsync($"/api/v1/products/", stringContent);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }
        #endregion


    }
}

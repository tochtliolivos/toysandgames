using System.Net;
using System.Net.Http.Json;
using Unosquare.ToysAndGames.Models.Dtos;
using Xunit.Abstractions;
using Unosquare.ToysAndGamesTest.Extensions;
using Newtonsoft.Json;
using Unosquare.ToysAndGamesTest.Fixtures;

namespace Unosquare.ToysAndGamesTest
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public IntegrationTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _output = output;
        }

        [Fact]
        public async void GetAllProducts()
        {
              //Act
            var response = await _client.GetAsync("api/Products");

            //Assert
            Assert.True(response.IsSuccessStatusCode);                
            Assert.NotNull(response);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);
            _output.WriteLine(responseContent.AsJson());           
        }

        [Fact]
        public async void CreateProduct_Ok()
        {
            //Arrange
            var product = new ProductDto()
            {
                Name = "Interegation Product",
                Description = "Integration test description",
                AgeRestriction = 20,
                CompanyId = 1,
                Price = 750.00M
            };

            //Act            
            var response = await _client.PostAsJsonAsync("api/Products", product);

            //Assert            
            Assert.True(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var ProductResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            Assert.NotNull(ProductResponse);
            Assert.Equal(product.Name, ProductResponse.Name);
        }

        [Fact]
        public async void CreateProduct_BadRequest()
        {
            //Arrange
            var product = new ProductDto()
            {
                Name = "Some product",
                Description = "Some Description",
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/Products", product);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }        

        [Fact]
        public async void UpdateProduct_OK()
        {
            var product = new ProductDto()
            {
                Id = 1,
                Name = "Integration Product",
                Description = "Integration test description",
                AgeRestriction = 20,
                CompanyId = 2,
                Price = 750.00M
            };

            var response = await _client.PutAsJsonAsync("api/Products", product);

            Assert.True(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var ProductResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            Assert.NotNull(ProductResponse);
            Assert.Equal(product.Name, ProductResponse.Name);
        }

        [Fact]
        [Trait("Product", "Test With Errors")]
        public async void UpdateProduct_Error()
        {
            var product = new ProductDto()
            {
                Id=10,
                Name = "Interegation Product",
                Description = "Integration test description",
                AgeRestriction = 20,
                CompanyId = 1,
                Price = 750.00M
            };

            var response = await _client.PutAsJsonAsync("api/Products", product);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }  
    }
}

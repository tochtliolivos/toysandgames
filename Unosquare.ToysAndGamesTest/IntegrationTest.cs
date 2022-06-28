using System.Net;
using System.Net.Http.Json;
using Unosquare.ToysAndGames.Models.Dtos;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Unosquare.ToysAndGamesTest.Extensions;

namespace ToysAndGamesTest
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
            try
            {
                //Act
                var response = await _client.GetAsync("api/Products");

                //Assert

                Assert.True(response.IsSuccessStatusCode);
                //TODO: We are evaluatiing that the response is a JSON but not if the JSON has information.
                //TODO: Assert should be that the products are not null at least, that the reponse was in the 200 range, etc., 
                Assert.NotNull(response);
                Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.NotNull(responseContent);
                _output.WriteLine(responseContent.AsJson());
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Status code was not success: {ex.Message}");
            }
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
                Company = "Integreation test company",
                Price = 750.00M
            };

            //Act            
            var response = await _client.PostAsJsonAsync("api/Products", product);

            //Assert
            //TODO: Missing Assert.IsTrue response.IsSuccessStatusCode or 
            //Assert.IsTrue(response.IsSuccessStatusCode)
            //Or checking if doesnt throw an exception via Record.Exception 
            //https://peterdaugaardrasmussen.com/2019/10/27/xunit-how-to-check-if-a-call-does-not-throw-an-exception/
            //var exception = Record.Exception(() => response.EnsureSuccessStatusCode());
            //Assert.Null(exception)
            response.EnsureSuccessStatusCode();

            //TODO: There is no assertion here
        }

        [Fact]
        public async void CreateProduct_BadRequest()
        {
            //Arrange
            var product = new ProductDto(){};

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
                Name = "Interegation Product",
                Description = "Integration test description",
                AgeRestriction = 20,
                Company = "Integreation test company",
                Price = 750.00M
            };

            var response = await _client.PutAsJsonAsync("api/Products", product);
            //TODO: Same here as the GOTO: Ln 50
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void UpdateProduct_Error()
        {
            var product = new ProductDto()
            {
                Name = "Interegation Product",
                Description = "Integration test description",
                AgeRestriction = 20,
                Company = "Integreation test company",
                Price = 750.00M
            };

            var response = await _client.PutAsJsonAsync("api/Products", product);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }  
    }
}

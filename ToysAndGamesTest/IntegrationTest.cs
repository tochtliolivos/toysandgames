using DBService;
using DBService.Entities;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using System.Net;
using System.Net.Http.Json;

namespace ToysAndGamesTest
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public IntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void GetAllProducts()
        {
            //Act
            var response = await _client.GetAsync("api/Products");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
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
            response.EnsureSuccessStatusCode();
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

using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Moq.EntityFrameworkCore;
using Unosquare.ToysAndGames.DBService.Entities;
using Unosquare.ToysAndGames.Models.Dtos;
using Unosquare.ToysAndGames.DBService;
using Unosquare.ToysAndGames.Services;
using Xunit.Abstractions;
using Unosquare.ToysAndGamesTest.Fixtures;

namespace Unosquare.ToysAndGamesTest
{
    public class ServicesUnitTest : IClassFixture<ProductsFixture>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ProductsFixture _productFixture;
        public ServicesUnitTest(ITestOutputHelper testOutputHelper, 
            ProductsFixture productFixture)
        {
            _testOutputHelper = testOutputHelper;
            _productFixture = productFixture;
        }

        [Fact]
        public async void GetAllProducts_RetunsProduct()
        {
            //Arrange
            var service = _productFixture.CreateProductService();

            //Act
            var products = await service.GetAllProducts();

            //Assert
            _testOutputHelper.WriteLine("GetAllProductos");
            Assert.Equal(2, products.Count());
            
        }

        [Fact]
        [Trait("Product", "Test With Errors")]
        public async void GetAllProducts_WithError()
        {
            //Arrange            
            var mapper = new Mock<IMapper>();

            mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Throws(new Exception(_productFixture.ErrorMessage));

            var service = _productFixture.CreateProductService(mapper.Object);

            //Act
            var exception = await Record.ExceptionAsync(async ()=> await service.GetAllProducts());

            Assert.NotNull(exception);
            Assert.Equal(_productFixture.ErrorMessage, exception.Message);
        }

        [Fact]
        public async void CreateProduct_WithNoError()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Product>>();
            var dbContextMock = new Mock<ToysAndGamesContext>();
            dbContextMock.Setup(p => p.Products).Returns(mockSet.Object);

            var loggerMock = new Mock<ILogger<ProductService>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            var mapper = config.CreateMapper();

            var toysAndGamesService = new ProductService(dbContextMock.Object,
                loggerMock.Object, mapper);

            //Act
            var result = await toysAndGamesService.CreateProduct(new ProductDto());

            //Assert
            Assert.Equal(result, new ProductDto());
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        [Trait("Product", "Test With Errors")]
        public async void CreateProduct_WithError()
        {
            //Arrange            
            var mapper = new Mock<IMapper>();

            mapper.Setup(m => m.Map<Product>(It.IsAny<ProductDto>()))
                .Throws(new Exception(_productFixture.ErrorMessage));

            var service = _productFixture.CreateProductService(mapper.Object);

            //Act
            var exception = await Record.ExceptionAsync(async() => await service.CreateProduct(new ProductDto()));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal(_productFixture.ErrorMessage, exception.Message);            
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Delete_Products_Ok(int id)
        {
            //Assert
            var service = _productFixture.CreateProductService();

            //Act
            var result = await service.DeleteProduct(id);

            //Assert
            Assert.True(result);
            //dbContextMock.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once);
            //dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }


}
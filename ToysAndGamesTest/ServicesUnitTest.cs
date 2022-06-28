using Services;
using Moq;
using Microsoft.EntityFrameworkCore;
using DBService;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DBService.Entities;
using Models.Models;
using Moq.EntityFrameworkCore;

namespace ToysAndGamesTest
{
    public class ServicesUnitTest
    {
        [Fact]
        //TODO: Review Fixture Class
        //TODO: Review Trait
        public void GetAllProducts_RetunsProduct()
        {
            //TODO: What is this test, testing? 

            //Arrange
            var data = new List<Product>{ new Product (), new Product () };
            var dtos = new List<ProductDto>{ new ProductDto(), new ProductDto()};
            var dataSet = GetQueryableMockDbSet(data);
            var dbContextMock = new Mock<ToysAndGamesContext>();
            dbContextMock.Setup(p => p.Products).ReturnsDbSet(dataSet.Object);

            var loggerMock = new Mock<ILogger<ToysAndGamesService>>();

            var mapper = new Mock<IMapper>();

            mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(dtos);
            
            //If we don't want to use de mock of the mapper
            /*var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            var mapper = config.CreateMapper();*/

            var toysAndGamesService = new ToysAndGamesService(dbContextMock.Object, 
                loggerMock.Object, mapper.Object);

            //Act
            var products = toysAndGamesService.GetAllProducts();
            
            //Assert
            Assert.Equal(2, products.Count());
            
        }

        [Fact]
        [Trait("Product", "Negative Testing")]
        public void GetAllProducts_WithError()
        {
            //Arrange
            var dbContextMock = new Mock<ToysAndGamesContext>();
            var loggerMock = new Mock<ILogger<ToysAndGamesService>>();            
            var mapper = new Mock<IMapper>();

            mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Throws(new Exception("Test Exception"));

            var toysAndGamesService = new ToysAndGamesService(dbContextMock.Object, loggerMock.Object, mapper.Object);

            //Act
            var products = toysAndGamesService.GetAllProducts();

            //Assert
            Assert.Empty(products);
          
        }

        [Fact]
        public void CreateProduct_WithNoError()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Product>>();
            var dbContextMock = new Mock<ToysAndGamesContext>();
            dbContextMock.Setup(p => p.Products).Returns(mockSet.Object);

            var loggerMock = new Mock<ILogger<ToysAndGamesService>>();
            var mapper = new Mock<IMapper>();

            var toysAndGamesService = new ToysAndGamesService(dbContextMock.Object, 
                loggerMock.Object, mapper.Object);

            //Act
            var result = toysAndGamesService.CreateProduct(new ProductDto());

            //Assert
            Assert.True(result);
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        [Trait("Product", "Negative Testing")]
        public void CreateProduct_WithError()
        {
            //Arrange            
            var dbContextMock = new Mock<ToysAndGamesContext>();

            var loggerMock = new Mock<ILogger<ToysAndGamesService>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Product>(It.IsAny<ProductDto>())).
                Throws(new Exception("Test Exception"));

            var toysAndGamesService = new ToysAndGamesService(dbContextMock.Object, 
                loggerMock.Object, mapper.Object);

            //Act
            var result = toysAndGamesService.CreateProduct(new ProductDto());

            //Assert
            Assert.False(result);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Delete_Products_Ok(int id)
        {
            //Assert
            var data = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };            
            var dataSet = GetQueryableMockDbSet(data);
            var dbContextMock = new Mock<ToysAndGamesContext>();
            dbContextMock.Setup(p => p.Products).ReturnsDbSet(dataSet.Object);            

            var loggerMock = new Mock<ILogger<ToysAndGamesService>>();
            var mapper = new Mock<IMapper>();


            var toysAndGamesService = new ToysAndGamesService(dbContextMock.Object,
                loggerMock.Object, mapper.Object);

            //Act
            var result = toysAndGamesService.DeleteProduct(id);

            //Assert
            Assert.True(result);
            dbContextMock.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));            

            return dbSet;
        }
    }


}
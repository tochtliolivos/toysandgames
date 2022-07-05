using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.ToysAndGames.DBService;
using Unosquare.ToysAndGames.DBService.Entities;
using Unosquare.ToysAndGames.Models.Contracts;
using Unosquare.ToysAndGames.Models.Dtos;
using Unosquare.ToysAndGames.Services;

namespace Unosquare.ToysAndGamesTest.Fixtures
{
    public class ProductsFixture
    {
        internal List<Product> GetEmptyProducts =>
            new List<Product> {
                new Product
                {
                    Id=1,
                    Name = "Test1",
                    Description = "Test Description 1",
                    Price = 100,
                    CompanyId = 1,
                    AgeRestriction = 10
                },
                new Product {
                    Id=2,
                    Name = "Test1",
                    Description = "Test Description 1",
                    Price = 101,
                    CompanyId = 2,
                    AgeRestriction = 11
                } };

        public string ErrorMessage => "Test Exception";
        
        internal Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
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

        public IToysAndGamesService CreateProductService(IMapper ?mapper = null)
        {
            var data = GetEmptyProducts;
            var dataSet = GetQueryableMockDbSet(data);
            var dbContextMock = new Mock<ToysAndGamesContext>();
            dbContextMock.Setup(p => p.Products).ReturnsDbSet(dataSet.Object);

            var loggerMock = new Mock<ILogger<ProductService>>();

            if (mapper is null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MapperConfig>();
                });
                mapper = config.CreateMapper();
            }

            return new ProductService(dbContextMock.Object,
                loggerMock.Object, mapper);
        }
        
    }
    
}

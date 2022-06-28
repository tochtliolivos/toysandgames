using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Unosquare.ToysAndGames.DBService;
using Unosquare.ToysAndGames.DBService.Entities;

namespace ToysAndGamesTest
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                typeof(DbContextOptions<ToysAndGamesContext>));

                services.Remove(descriptor);
                services.AddDbContext<ToysAndGamesContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ToysAndGamesContext>();
                using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                    .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());
                ILogger logger = loggerFactory.CreateLogger<Program>();
                try
                {
                    db.Database.EnsureCreated();
                    InitializeDbForTests(db);
                }
                catch(Exception e)
                {
                    logger.LogError("Error with seed information: " + e.Message);
                }                
            });
        }

        internal static void InitializeDbForTests(ToysAndGamesContext db)
        {
            db.Products.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        //TODO: Is this being used? 
        internal static void ReinitializeDbForTests(ToysAndGamesContext db)
        {
            db.Products.RemoveRange(db.Products);
            InitializeDbForTests(db);
        }

        internal static List<Product> GetSeedingMessages()
        {
            return new List<Product>()
            {
                new Product(){ 
                    Id=1,
                    Name = "test1",
                    Description = "Description1",
                    Company ="a",
                    AgeRestriction = 20,
                    Price = 101.0M
                }
            };
        }
    }
}

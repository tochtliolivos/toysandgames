using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unosquare.ToysAndGames.DBService;
using Unosquare.ToysAndGames.Models.Contracts;
using Unosquare.ToysAndGames.Models.Dtos;
using Unosquare.ToysAndGames.Models.Validators;

namespace Unosquare.ToysAndGames.Services
{
    public static class ProductServiceDependencies
    {
        public static IServiceCollection AddProductServiceDependencies(this  IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToysAndGamesContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ToysAndGamesConnection")
            ));
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped<IValidator<ProductDto>, ProductValidator>();
            services.AddScoped<IToysAndGamesService, ProductService>();
            return services;
        }
    }
}

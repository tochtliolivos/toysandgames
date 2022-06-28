using AutoMapper;
using Unosquare.ToysAndGames.DBService.Entities;
using Unosquare.ToysAndGames.Models.Dtos;

namespace Unosquare.ToysAndGames.Services
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}

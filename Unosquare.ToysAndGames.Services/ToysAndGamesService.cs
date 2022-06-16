using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unosquare.ToysAndGames.Models.Contracts;
using Unosquare.ToysAndGames.Models.Dtos;
using Unosquare.ToysAndGames.DBService.Entities;
using Unosquare.ToysAndGames.DBService;

namespace Unosquare.ToysAndGames.Services
{
    public class ToysAndGamesService : IToysAndGamesService
    {
        private readonly ToysAndGamesContext _toyAndGamesContext;
        private readonly ILogger<ToysAndGamesService> _logger;
        private readonly IMapper _mapper;

        public ToysAndGamesService(ToysAndGamesContext toysAndGamesContext, 
            ILogger<ToysAndGamesService> logger,
            IMapper mapper)
        {
            _toyAndGamesContext = toysAndGamesContext;
            _logger = logger;
            _mapper = mapper;
        }

        public bool CreateProduct(ProductDto product)
        {
            try
            {
                _toyAndGamesContext.Products.Add(_mapper.Map<Product>(product));
                _toyAndGamesContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("Create Product " + e.Message);
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _toyAndGamesContext.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    _logger.LogWarning("delete: productid: " + id + "not exist");
                    return false;
                }
                _toyAndGamesContext.Remove(product);
                _toyAndGamesContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("Delete productid: " + e.Message);
                return false;
            }
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            try
            {               
                return _mapper.Map<IEnumerable<ProductDto>>(_toyAndGamesContext.Products);
            }
            catch (Exception e)
            {
                _logger.LogError("Error fetching products" + e.Message);
                return Enumerable.Empty<ProductDto>();
            }
            
        }

        public bool UpdateProduct(ProductDto product)
        {
            try
            {
                var oldProduct = _toyAndGamesContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
                if (oldProduct == null)
                {
                    _logger.LogWarning("update: productid " + product.Id + "does not exist");
                    return false;
                }
                _toyAndGamesContext.Products.Update(_mapper.Map<Product>(product));
                _toyAndGamesContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Update Producty: " + e.Message);
                return false;
            }            
        }
    }
}

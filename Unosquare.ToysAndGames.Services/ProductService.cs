using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unosquare.ToysAndGames.Models.Contracts;
using Unosquare.ToysAndGames.Models.Dtos;
using Unosquare.ToysAndGames.DBService.Entities;
using Unosquare.ToysAndGames.DBService;

namespace Unosquare.ToysAndGames.Services
{
    public class ProductService : IToysAndGamesService
    {
        private readonly ToysAndGamesContext _toyAndGamesContext;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(ToysAndGamesContext toysAndGamesContext,
            ILogger<ProductService> logger,
            IMapper mapper)
        {
            _toyAndGamesContext = toysAndGamesContext;
            _logger = logger;
            _mapper = mapper;            
        }
        
        public Task<ProductDto> CreateProduct(ProductDto product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                _toyAndGamesContext.Products.Add(productEntity);
                _toyAndGamesContext.SaveChanges();
                return Task.FromResult(_mapper.Map<ProductDto>(productEntity));
            }
            catch (Exception e)
            {
                _logger.LogError($"Create Product failed: {e.Message}");
                throw;
            }
        }

        public Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = _toyAndGamesContext.Products.First(el=>el.Id == id);
                if (product is not null)
                {
                    _toyAndGamesContext.Remove(product);
                    _toyAndGamesContext.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception e)
            {
                _logger.LogError($"Delete productid: {id} message: {e.Message}");
                throw;
            }
        }

        public Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            try
            {
                var products = _toyAndGamesContext.Products.AsNoTracking();
                var prodcutsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Task.FromResult(prodcutsDto);
            }
            catch (Exception e)
            {
                _logger.LogError("Error fetching products" + e.Message);
                throw;
            }
        }

        public Task<ProductDto> UpdateProduct(ProductDto product)
        {
            try
            {
                var productToUpdate = _mapper.Map<Product>(product);
                _toyAndGamesContext.Products.Update(productToUpdate);
                _toyAndGamesContext.SaveChanges();
                return Task.FromResult(product);
            }
            catch (Exception e)
            {
                _logger.LogError("Update Producty: " + e.Message);
                throw;
            }
        }        
    }
}

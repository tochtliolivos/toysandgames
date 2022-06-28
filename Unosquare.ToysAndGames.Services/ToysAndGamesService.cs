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

        //TODO: Good practices are that the service returns a Task<T> and make the methods async, and should return the object created
        //Updates and creates should return a resource representation.
        //https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api#useful-post-responses
        //To prevent an API consumer from having to hit the API again for an updated representation,
        //have the API return the updated (or created) representation as part of the response.
        public bool CreateProduct(ProductDto product)
        {
            try
            {
                //IdentitySpecification / AutoIncrement
                _toyAndGamesContext.Products.Add(_mapper.Map<Product>(product));
                _toyAndGamesContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //TODO:use string interpolation for all the concatenarions
                _logger.LogError($"Create Product failed: {e.Message}");
                throw;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _toyAndGamesContext.Products.Find(id); // Best Performance
                //var product = _toyAndGamesContext.Products.FirstOrDefault(p => p.Id == id); //TODO: This could also be a Find with catching exception
                //if (product == null)//TODO: just for start getting involved in the new ways, lets use Is null or is not null for readability
                //{
                //    _logger.LogWarning("Delete: productid: " + id + "not exist");
                //    return false;
                //}
                _toyAndGamesContext.Remove(product);
                _toyAndGamesContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Delete productid: " + e.Message);
                return false;
            }
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            try
            {
                //TODO: This one could use the AsNoTracking
                return _mapper.Map<IEnumerable<ProductDto>>(_toyAndGamesContext.Products.AsNoTracking());
            }
            catch (Exception e)
            {
                _logger.LogError("Error fetching products" + e.Message);
                //TODO: Why not returning null?
                return null;
            }

        }

        public bool UpdateProduct(ProductDto product)
        {
            try
            {
                //TODO: Im not sure about this .... lets talk about it 
                //var oldProduct = _toyAndGamesContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
                //if (oldProduct == null)
                //{
                //    _logger.LogWarning("Update: productid " + product.Id + "does not exist");
                //    return false;
                //}
                var productToUpdate = _toyAndGamesContext.Products.Find(product.Id);
                if (productToUpdate is not null) // != null 
                {
                    productToUpdate.Name = product.Name;
                    //_mapper.Map<Product>(product)
                    _toyAndGamesContext.Products.Update(productToUpdate);
                    _toyAndGamesContext.SaveChanges();
                    return true;
                }
                throw new Exception($"Product was not found in the database, id:  {product.Id}");

            }
            catch (Exception e)
            {
                _logger.LogError("Update Producty: " + e.Message);
                return false;
            }
        }
    }
}

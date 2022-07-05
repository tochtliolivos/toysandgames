using Unosquare.ToysAndGames.Models.Dtos;

namespace Unosquare.ToysAndGames.Models.Contracts
{
    public interface IToysAndGamesService
    {
        public Task<IEnumerable<ProductDto>> GetAllProducts();
        public Task<ProductDto> CreateProduct(ProductDto product);
        public Task<ProductDto> UpdateProduct(ProductDto product);
        public Task<bool> DeleteProduct(int id);
    }
}

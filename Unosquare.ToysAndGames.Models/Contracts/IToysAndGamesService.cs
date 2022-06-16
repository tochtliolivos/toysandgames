using Unosquare.ToysAndGames.Models.Dtos;

namespace Unosquare.ToysAndGames.Models.Contracts
{
    public interface IToysAndGamesService
    {
        public IEnumerable<ProductDto> GetAllProducts();
        public bool CreateProduct(ProductDto product);
        public bool UpdateProduct(ProductDto product);
        public bool DeleteProduct(int id);
    }
}

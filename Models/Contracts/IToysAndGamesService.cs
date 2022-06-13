using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Contracts
{
    public interface IToysAndGamesService
    {
        public IEnumerable<ProductDto> GetAllProducts();
        public bool CreateProduct(ProductDto product);
        public bool UpdateProduct(ProductDto product);
        public bool DeleteProduct(int id);
    }
}

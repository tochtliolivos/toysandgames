using System.ComponentModel.DataAnnotations;

namespace Unosquare.ToysAndGames.Models.Dtos
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public int AgeRestriction { get; set; }
        public int CompanyId { get; set; }
        public decimal Price { get; set; }
    }
}

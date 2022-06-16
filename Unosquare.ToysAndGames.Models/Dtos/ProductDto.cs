using System.ComponentModel.DataAnnotations;

namespace Unosquare.ToysAndGames.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0,100)]
        public int AgeRestriction { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [Range(1,1000)]
        public decimal Price { get; set; }
    }
}

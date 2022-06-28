using System.ComponentModel.DataAnnotations;

namespace Unosquare.ToysAndGames.Models.Dtos
{
    //TODO: Use record instead of Class for DTOS
    public record ProductDto
    {
        public int Id { get; set; }

        [Required] //TODO: on DTOs lets try to use FluentValidations instead of DataAnnotations
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0,100)]
        public int AgeRestriction { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [Range(1,1000)] //TODO: this one cant be converted to fluentValudation Where or FluentAPI restriction, still we could create the CK_Constraint Manually.
        public decimal Price { get; set; }
    }
}

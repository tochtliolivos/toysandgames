using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
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

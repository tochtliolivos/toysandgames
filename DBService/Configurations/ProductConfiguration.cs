using DBService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //TODO: Lets move this to an IEntityTypeConfiguration<T>
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).
                IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).
                IsRequired().HasMaxLength(100);
            builder.Property(p => p.AgeRestriction).IsRequired();
            builder.Property(p => p.Company).
                IsRequired().HasMaxLength(50);
            builder.Property(p => p.Price).IsRequired();
            //builder.HasOne(x=> x.Product).WithMany(x=> Companies);

            builder.HasData(Get());

        }

        public IList<Product> Get()
        {
            return new List<Product>() {
                new Product
                {
                    Id = 1,
                    Name = "Barbie Developer",
                    Description = "lorem ipsum dolor",
                    AgeRestriction = 12,
                    Company = "Mattel",
                    Price = 300.00M
                },
                new Product
                {
                    Id = 2,
                    Name = "Pista Hotweels",
                    Description = "Pista con dos coches etc",
                    AgeRestriction = 15,
                    Company = "Mattel",
                    Price = 650.00M
                },
                new Product
                {
                    Id = 3,
                    Name = "Adivina quien",
                    Description = "Juego de mesa para dos personas",
                    AgeRestriction = 99,
                    Company = "Hasbro",
                    Price = 250.00M
                }
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unosquare.ToysAndGames.DBService.Entities;

namespace Unosquare.ToysAndGames.DBService.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Name).
                IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).
                IsRequired().HasMaxLength(100);
            builder.Property(p => p.AgeRestriction).IsRequired();            
            builder.Property(p => p.Price).IsRequired();
            builder.HasOne(p => p.Company).WithMany(p => p.Products)
                .HasForeignKey(p => p.CompanyId);
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
                    CompanyId = 1,
                    Price = 300.00M
                },
                new Product
                {
                    Id = 2,
                    Name = "Pista Hotweels",
                    Description = "Pista con dos coches etc",
                    AgeRestriction = 15,
                    CompanyId = 1,
                    Price = 650.00M
                },
                new Product
                {
                    Id = 3,
                    Name = "Adivina quien",
                    Description = "Juego de mesa para dos personas",
                    AgeRestriction = 99,
                    CompanyId = 2,
                    Price = 250.00M
                }
            };
        }
    }
}

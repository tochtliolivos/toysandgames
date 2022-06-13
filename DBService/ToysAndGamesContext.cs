using DBService.Entities;
using Microsoft.EntityFrameworkCore;


namespace DBService
{
    public class ToysAndGamesContext : DbContext
    {
        public ToysAndGamesContext(){}
        public ToysAndGamesContext(DbContextOptions<ToysAndGamesContext> options) 
            : base(options) { }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).
                IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Description).
                IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.AgeRestriction).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Company).
                IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();


            modelBuilder.Entity<Product>().HasData(
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
                    AgeRestriction=15,
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
            );
        }
    }
}

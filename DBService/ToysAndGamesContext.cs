using DBService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

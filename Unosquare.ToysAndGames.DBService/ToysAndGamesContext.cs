using Microsoft.EntityFrameworkCore;
using Unosquare.ToysAndGames.DBService.Entities;
using System.Reflection;

namespace Unosquare.ToysAndGames.DBService
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

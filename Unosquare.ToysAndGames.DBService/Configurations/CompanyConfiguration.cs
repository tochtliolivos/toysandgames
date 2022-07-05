using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.ToysAndGames.DBService.Entities;

namespace Unosquare.ToysAndGames.DBService.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id); ;
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(100);            
            builder.HasData(GetData());
        }

        public IList<Company> GetData()
        {
            return new List<Company>()
            {
                new Company
                {
                    Id = 1,
                    Name = "Mattel"
                },
                new Company
                {
                    Id = 2,
                    Name = "Hasbro"
                }
            };
        }
    }
}

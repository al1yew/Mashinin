using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.NameAz).IsRequired();
            builder.Property(x => x.NameRu).IsRequired();
            builder.Property(x => x.NameEn).IsRequired();
        }
    }
}

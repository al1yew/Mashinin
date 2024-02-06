using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.Property(x => x.HexCode).IsRequired().HasMaxLength(7);
            builder.Property(x => x.NameAz).IsRequired();
            builder.Property(x => x.NameEn).IsRequired();
            builder.Property(x => x.NameRu).IsRequired();
        }
    }
}

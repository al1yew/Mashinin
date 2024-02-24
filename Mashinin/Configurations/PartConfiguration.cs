using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.Property(x => x.Heading).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.MainImage).IsRequired();
            builder.Property(x => x.PartCategory).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.PartSpecificationId).IsRequired();
        }
    }
}

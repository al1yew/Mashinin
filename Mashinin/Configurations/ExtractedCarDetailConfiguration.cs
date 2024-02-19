using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class ExtractedCarDetailConfiguration : IEntityTypeConfiguration<ExtractedCarDetail>
    {
        public void Configure(EntityTypeBuilder<ExtractedCarDetail> builder)
        {
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Odometer).IsRequired();
            builder.Property(x => x.MakeId).IsRequired();
            builder.Property(x => x.ModelId).IsRequired();
            builder.Property(x => x.TurboAzModelId).IsRequired();
            builder.Property(x => x.TurboAzMakeId).IsRequired();
            builder.Property(x => x.Year).IsRequired();

        }
    }
}

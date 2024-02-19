using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class ExtractedNumberConfiguration : IEntityTypeConfiguration<ExtractedNumber>
    {
        public void Configure(EntityTypeBuilder<ExtractedNumber> builder)
        {
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(13);
            builder.Property(x => x.TurboAzMakeId).IsRequired();
            builder.Property(x => x.TurboAzModelId).IsRequired();
            builder.Property(x => x.ModelId).IsRequired();
            builder.Property(x => x.MakeId).IsRequired();
            builder.Property(x => x.Link).IsRequired();
        }
    }
}

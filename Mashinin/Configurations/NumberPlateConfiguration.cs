using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class NumberPlateConfiguration : IEntityTypeConfiguration<NumberPlate>
    {
        public void Configure(EntityTypeBuilder<NumberPlate> builder)
        {
            builder.Property(x => x.Value).IsRequired().HasMaxLength(7);
            builder.Property(x => x.Description).IsRequired();
        }
    }
}

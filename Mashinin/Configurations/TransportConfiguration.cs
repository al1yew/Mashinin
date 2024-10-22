﻿using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mashinin.Configurations
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.Property(x => x.MakeId).IsRequired();
            builder.Property(x => x.ModelId).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.ImagesFolderId).IsRequired();
            builder.Property(x => x.ColorId).IsRequired();
            builder.Property(x => x.AdType).IsRequired();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Odometer).IsRequired();
            builder.Property(x => x.PersonPlacesCount).IsRequired();
            builder.Property(x => x.EngineVolume).IsRequired();
            builder.Property(x => x.EnginePower).IsRequired();
            builder.Property(x => x.FrontImage).IsRequired();
            builder.Property(x => x.RearImage).IsRequired();
            builder.Property(x => x.FrontImageLowResolution).IsRequired();
            builder.Property(x => x.RearImageLowResolution).IsRequired();
            builder.Property(x => x.FuelType).IsRequired();
            builder.Property(x => x.DrivingWheels).IsRequired();
            builder.Property(x => x.TransmissionType).IsRequired();
            builder.Property(x => x.BodyType).IsRequired();
        }
    }
}

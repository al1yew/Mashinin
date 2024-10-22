﻿// <auto-generated />
using System;
using Mashinin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mashinin.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mashinin.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<string>("NameAz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Mashinin.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("HexCode")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<string>("NameAz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Mashinin.Entities.ExtractedCarDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineVolume")
                        .HasColumnType("int");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("Odometer")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PostCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("TurboAzMakeId")
                        .HasColumnType("int");

                    b.Property<int>("TurboAzModelId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExtractedCarDetails");
                });

            modelBuilder.Entity("Mashinin.Entities.ExtractedNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("TurboAzMakeId")
                        .HasColumnType("int");

                    b.Property<int>("TurboAzModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExtractedNumbers");
                });

            modelBuilder.Entity("Mashinin.Entities.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TurboAzId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("Mashinin.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TurboAzId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Mashinin.Entities.NumberPlate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForBargain")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NumberPlates");
                });

            modelBuilder.Entity("Mashinin.Entities.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<int>("TransportId")
                        .HasColumnType("int");

                    b.Property<double?>("TransportationPrice")
                        .HasColumnType("float");

                    b.Property<double?>("Value")
                        .HasColumnType("float");

                    b.Property<double?>("WinPriceMax")
                        .HasColumnType("float");

                    b.Property<double?>("WinPriceMin")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TransportId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Mashinin.Entities.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdType")
                        .HasColumnType("int");

                    b.Property<int>("BodyType")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrivingWheels")
                        .HasColumnType("int");

                    b.Property<int>("EnginePower")
                        .HasColumnType("int");

                    b.Property<int>("EngineVolume")
                        .HasColumnType("int");

                    b.Property<int?>("ExporterCountry")
                        .HasColumnType("int");

                    b.Property<string>("FrontImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FrontImageLowResolution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FuelConsumptionAverage")
                        .HasColumnType("float");

                    b.Property<double?>("FuelConsumptionCity")
                        .HasColumnType("float");

                    b.Property<double?>("FuelConsumptionHighway")
                        .HasColumnType("float");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<bool>("Has360Camera")
                        .HasColumnType("bit");

                    b.Property<bool>("HasABS")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAUX")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAirConditioning")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAlloyWheels")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAndroidAuto")
                        .HasColumnType("bit");

                    b.Property<bool>("HasAppleCarPlay")
                        .HasColumnType("bit");

                    b.Property<bool>("HasBluetooth")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCentralLocking")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCruiseControl")
                        .HasColumnType("bit");

                    b.Property<bool>("HasElectronicWindows")
                        .HasColumnType("bit");

                    b.Property<bool>("HasFoggyHeadlights")
                        .HasColumnType("bit");

                    b.Property<bool>("HasGasCylinder")
                        .HasColumnType("bit");

                    b.Property<bool>("HasHandGearChange")
                        .HasColumnType("bit");

                    b.Property<bool>("HasHook")
                        .HasColumnType("bit");

                    b.Property<bool>("HasIsofix")
                        .HasColumnType("bit");

                    b.Property<bool>("HasKeylessGo")
                        .HasColumnType("bit");

                    b.Property<bool>("HasLeatherSalon")
                        .HasColumnType("bit");

                    b.Property<bool>("HasMemoryPackage")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPanoramaRoof")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParkingAssistant")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParkingRadar")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPneumaticSuspension")
                        .HasColumnType("bit");

                    b.Property<bool>("HasRainSensor")
                        .HasColumnType("bit");

                    b.Property<bool>("HasRearViewCamera")
                        .HasColumnType("bit");

                    b.Property<bool>("HasScreen")
                        .HasColumnType("bit");

                    b.Property<bool>("HasSeatHeating")
                        .HasColumnType("bit");

                    b.Property<bool>("HasSeatVentilation")
                        .HasColumnType("bit");

                    b.Property<bool>("HasSideCurtains")
                        .HasColumnType("bit");

                    b.Property<bool>("HasStartStop")
                        .HasColumnType("bit");

                    b.Property<bool>("HasSunroof")
                        .HasColumnType("bit");

                    b.Property<bool>("HasTractionControl")
                        .HasColumnType("bit");

                    b.Property<bool>("HasTrunkReleaseButton")
                        .HasColumnType("bit");

                    b.Property<bool>("HasUSB")
                        .HasColumnType("bit");

                    b.Property<bool>("HasWirelessCharging")
                        .HasColumnType("bit");

                    b.Property<int?>("HeadlightType")
                        .HasColumnType("int");

                    b.Property<int>("ImagesFolderId")
                        .HasColumnType("int");

                    b.Property<bool>("ImportTaxesPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBarter")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDamaged")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForBargain")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForParts")
                        .HasColumnType("bit");

                    b.Property<bool>("IsImported")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPremium")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRepainted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVip")
                        .HasColumnType("bit");

                    b.Property<string>("KeyWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("MetaDatas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<double>("Odometer")
                        .HasColumnType("float");

                    b.Property<int?>("OwnersCount")
                        .HasColumnType("int");

                    b.Property<int?>("PeriodOfTime")
                        .HasColumnType("int");

                    b.Property<int>("PersonPlacesCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PremiumExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("PriceForRepair")
                        .HasColumnType("float");

                    b.Property<string>("RearImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RearImageLowResolution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransmissionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VipExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ColorId");

                    b.HasIndex("MakeId");

                    b.HasIndex("ModelId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("Mashinin.Entities.TransportImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportId");

                    b.ToTable("TransportImages");
                });

            modelBuilder.Entity("Mashinin.Entities.Model", b =>
                {
                    b.HasOne("Mashinin.Entities.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");
                });

            modelBuilder.Entity("Mashinin.Entities.Price", b =>
                {
                    b.HasOne("Mashinin.Entities.Transport", "Transport")
                        .WithMany("Prices")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Mashinin.Entities.Transport", b =>
                {
                    b.HasOne("Mashinin.Entities.City", "City")
                        .WithMany("Transports")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mashinin.Entities.Color", "Color")
                        .WithMany("Transports")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mashinin.Entities.Make", "Make")
                        .WithMany("Transports")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mashinin.Entities.Model", "Model")
                        .WithMany("Transports")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Color");

                    b.Navigation("Make");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Mashinin.Entities.TransportImage", b =>
                {
                    b.HasOne("Mashinin.Entities.Transport", "Transport")
                        .WithMany("TransportImages")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Mashinin.Entities.City", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("Mashinin.Entities.Color", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("Mashinin.Entities.Make", b =>
                {
                    b.Navigation("Models");

                    b.Navigation("Transports");
                });

            modelBuilder.Entity("Mashinin.Entities.Model", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("Mashinin.Entities.Transport", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("TransportImages");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mashinin.Migrations
{
    /// <inheritdoc />
    public partial class addedTransportAndRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    ImagesFolderId = table.Column<int>(type: "int", nullable: false),
                    FrontImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontImageLowResolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RearImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RearImageLowResolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdType = table.Column<int>(type: "int", nullable: false),
                    PeriodOfTime = table.Column<int>(type: "int", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Odometer = table.Column<double>(type: "float", nullable: false),
                    OwnersCount = table.Column<int>(type: "int", nullable: true),
                    PersonPlacesCount = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineVolume = table.Column<int>(type: "int", nullable: false),
                    EnginePower = table.Column<int>(type: "int", nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDatas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceForRepair = table.Column<double>(type: "float", nullable: true),
                    FuelConsumptionAverage = table.Column<double>(type: "float", nullable: true),
                    FuelConsumptionCity = table.Column<double>(type: "float", nullable: true),
                    FuelConsumptionHighway = table.Column<double>(type: "float", nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    DrivingWheels = table.Column<int>(type: "int", nullable: false),
                    ExporterCountry = table.Column<int>(type: "int", nullable: true),
                    TransmissionType = table.Column<int>(type: "int", nullable: false),
                    BodyType = table.Column<int>(type: "int", nullable: false),
                    HeadlightType = table.Column<int>(type: "int", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsBarter = table.Column<bool>(type: "bit", nullable: false),
                    IsDamaged = table.Column<bool>(type: "bit", nullable: false),
                    IsRepainted = table.Column<bool>(type: "bit", nullable: false),
                    IsForParts = table.Column<bool>(type: "bit", nullable: false),
                    IsImported = table.Column<bool>(type: "bit", nullable: false),
                    ImportTaxesPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsForBargain = table.Column<bool>(type: "bit", nullable: false),
                    IsRunning = table.Column<bool>(type: "bit", nullable: false),
                    IsVip = table.Column<bool>(type: "bit", nullable: false),
                    VipExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasAlloyWheels = table.Column<bool>(type: "bit", nullable: false),
                    HasGasCylinder = table.Column<bool>(type: "bit", nullable: false),
                    HasFoggyHeadlights = table.Column<bool>(type: "bit", nullable: false),
                    HasBluetooth = table.Column<bool>(type: "bit", nullable: false),
                    HasScreen = table.Column<bool>(type: "bit", nullable: false),
                    Has360Camera = table.Column<bool>(type: "bit", nullable: false),
                    HasElectronicWindows = table.Column<bool>(type: "bit", nullable: false),
                    HasTrunkReleaseButton = table.Column<bool>(type: "bit", nullable: false),
                    HasUSB = table.Column<bool>(type: "bit", nullable: false),
                    HasABS = table.Column<bool>(type: "bit", nullable: false),
                    HasSunroof = table.Column<bool>(type: "bit", nullable: false),
                    HasRainSensor = table.Column<bool>(type: "bit", nullable: false),
                    HasCentralLocking = table.Column<bool>(type: "bit", nullable: false),
                    HasParkingRadar = table.Column<bool>(type: "bit", nullable: false),
                    HasAirConditioning = table.Column<bool>(type: "bit", nullable: false),
                    HasSeatHeating = table.Column<bool>(type: "bit", nullable: false),
                    HasLeatherSalon = table.Column<bool>(type: "bit", nullable: false),
                    HasRearViewCamera = table.Column<bool>(type: "bit", nullable: false),
                    HasSideCurtains = table.Column<bool>(type: "bit", nullable: false),
                    HasSeatVentilation = table.Column<bool>(type: "bit", nullable: false),
                    HasCruiseControl = table.Column<bool>(type: "bit", nullable: false),
                    HasTractionControl = table.Column<bool>(type: "bit", nullable: false),
                    HasAppleCarPlay = table.Column<bool>(type: "bit", nullable: false),
                    HasAndroidAuto = table.Column<bool>(type: "bit", nullable: false),
                    HasAUX = table.Column<bool>(type: "bit", nullable: false),
                    HasPanoramaRoof = table.Column<bool>(type: "bit", nullable: false),
                    HasKeylessGo = table.Column<bool>(type: "bit", nullable: false),
                    HasStartStop = table.Column<bool>(type: "bit", nullable: false),
                    HasMemoryPackage = table.Column<bool>(type: "bit", nullable: false),
                    HasHandGearChange = table.Column<bool>(type: "bit", nullable: false),
                    HasWirelessCharging = table.Column<bool>(type: "bit", nullable: false),
                    HasParkingAssistant = table.Column<bool>(type: "bit", nullable: false),
                    HasIsofix = table.Column<bool>(type: "bit", nullable: false),
                    HasPneumaticSuspension = table.Column<bool>(type: "bit", nullable: false),
                    HasHook = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transports_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinPriceMin = table.Column<double>(type: "float", nullable: true),
                    WinPriceMax = table.Column<double>(type: "float", nullable: true),
                    TransportationPrice = table.Column<double>(type: "float", nullable: true),
                    TransportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportImages_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_TransportId",
                table: "Prices",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportImages_TransportId",
                table: "TransportImages",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CityId",
                table: "Transports",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ColorId",
                table: "Transports",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_MakeId",
                table: "Transports",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ModelId",
                table: "Transports",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "TransportImages");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}

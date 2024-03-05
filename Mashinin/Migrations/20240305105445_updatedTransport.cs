using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mashinin.Migrations
{
    /// <inheritdoc />
    public partial class updatedTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdType",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodOfTime",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RentPrice",
                table: "Transports",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdType",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "PeriodOfTime",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "Transports");
        }
    }
}

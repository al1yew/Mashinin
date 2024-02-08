using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mashinin.Migrations
{
    /// <inheritdoc />
    public partial class updatedModel_AddedClassProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Models");
        }
    }
}

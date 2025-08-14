using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoLongitudeeLatitude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Bakeries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Bakeries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Bakeries");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Bakeries");
        }
    }
}

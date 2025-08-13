using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryRegistration.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacaocampouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Users");
        }
    }
}

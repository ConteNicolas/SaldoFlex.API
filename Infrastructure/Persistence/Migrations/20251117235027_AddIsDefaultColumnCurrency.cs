using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaldoFlex.API.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultColumnCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Currencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Currencies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaldoFlex.API.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCanDeleteProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

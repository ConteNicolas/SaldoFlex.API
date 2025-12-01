using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaldoFlex.API.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsToFinancialPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FinancialPlanId",
                table: "Tags",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_FinancialPlanId",
                table: "Tags",
                column: "FinancialPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_FinancialPlans_FinancialPlanId",
                table: "Tags",
                column: "FinancialPlanId",
                principalTable: "FinancialPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_FinancialPlans_FinancialPlanId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_FinancialPlanId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "FinancialPlanId",
                table: "Tags");
        }
    }
}

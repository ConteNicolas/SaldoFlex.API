using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaldoFlex.API.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanclaPlansToTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_FinancialPlans_FinancialPlanId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_FinancialPlanId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "FinancialPlanId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "FinancialPlanTag",
                columns: table => new
                {
                    FinancialPlansId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPlanTag", x => new { x.FinancialPlansId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FinancialPlanTag_FinancialPlans_FinancialPlansId",
                        column: x => x.FinancialPlansId,
                        principalTable: "FinancialPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialPlanTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialPlanTag_TagsId",
                table: "FinancialPlanTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialPlanTag");

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
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaafiSystems.Migrations
{
    public partial class AddOwnerExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Loads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RevenueCategoryID",
                table: "Loads",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OwnerExpenses",
                columns: table => new
                {
                    ExpenseID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerExpenses", x => new { x.ExpenseID, x.OwnerID });
                    table.ForeignKey(
                        name: "FK_OwnerExpenses_Expenses_ExpenseID",
                        column: x => x.ExpenseID,
                        principalTable: "Expenses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerExpenses_Owners_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loads_RevenueCategoryID",
                table: "Loads",
                column: "RevenueCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerExpenses_OwnerID",
                table: "OwnerExpenses",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loads_RevenueCategories_RevenueCategoryID",
                table: "Loads",
                column: "RevenueCategoryID",
                principalTable: "RevenueCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loads_RevenueCategories_RevenueCategoryID",
                table: "Loads");

            migrationBuilder.DropTable(
                name: "OwnerExpenses");

            migrationBuilder.DropIndex(
                name: "IX_Loads_RevenueCategoryID",
                table: "Loads");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Loads");

            migrationBuilder.DropColumn(
                name: "RevenueCategoryID",
                table: "Loads");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Web.Migrations
{
    public partial class tripEntityCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_ExpensesTypes_ExpensesTypeId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ExpensesTypeId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExpensesTypeId",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpensesTypeId",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ExpensesTypeId",
                table: "Trips",
                column: "ExpensesTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_ExpensesTypes_ExpensesTypeId",
                table: "Trips",
                column: "ExpensesTypeId",
                principalTable: "ExpensesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

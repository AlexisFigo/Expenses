using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Web.Migrations
{
    public partial class adddescriptiontotrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trips");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Web.Migrations
{
    public partial class changenamesintrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Trips_tripId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cities_CitieId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "CitieId",
                table: "Trips",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_CitieId",
                table: "Trips",
                newName: "IX_Trips_CityId");

            migrationBuilder.RenameColumn(
                name: "tripId",
                table: "TripDetails",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripDetails_tripId",
                table: "TripDetails",
                newName: "IX_TripDetails_TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Trips_TripId",
                table: "TripDetails",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cities_CityId",
                table: "Trips",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Trips_TripId",
                table: "TripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cities_CityId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Trips",
                newName: "CitieId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_CityId",
                table: "Trips",
                newName: "IX_Trips_CitieId");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "TripDetails",
                newName: "tripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripDetails_TripId",
                table: "TripDetails",
                newName: "IX_TripDetails_tripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Trips_tripId",
                table: "TripDetails",
                column: "tripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cities_CitieId",
                table: "Trips",
                column: "CitieId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

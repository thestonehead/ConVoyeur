using Microsoft.EntityFrameworkCore.Migrations;

namespace ConVoyeur.Web.Migrations
{
    public partial class AddedActivatedLocationToActivityEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivatedLocationId",
                table: "ActivityEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntries_ActivatedLocationId",
                table: "ActivityEntries",
                column: "ActivatedLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntries_Locations_ActivatedLocationId",
                table: "ActivityEntries",
                column: "ActivatedLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntries_Locations_ActivatedLocationId",
                table: "ActivityEntries");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntries_ActivatedLocationId",
                table: "ActivityEntries");

            migrationBuilder.DropColumn(
                name: "ActivatedLocationId",
                table: "ActivityEntries");
        }
    }
}

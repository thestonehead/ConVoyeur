using Microsoft.EntityFrameworkCore.Migrations;

namespace ConVoyeur.Web.Migrations
{
    public partial class AddedVisitorReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityEntryReview",
                columns: table => new
                {
                    ActivityEntryId = table.Column<int>(nullable: false),
                    Grade = table.Column<float>(nullable: false),
                    Review = table.Column<string>(nullable: true),
                    Access = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityEntryReview", x => x.ActivityEntryId);
                    table.ForeignKey(
                        name: "FK_ActivityEntryReview_ActivityEntries_ActivityEntryId",
                        column: x => x.ActivityEntryId,
                        principalTable: "ActivityEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityEntryReview");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class AddComplaintFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Contents_ContentId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ContentId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Complaints");

            migrationBuilder.CreateTable(
                name: "ComplaintContent",
                columns: table => new
                {
                    ComplaintsId = table.Column<int>(type: "int", nullable: false),
                    ContentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintContent", x => new { x.ComplaintsId, x.ContentsId });
                    table.ForeignKey(
                        name: "FK_ComplaintContent_Complaints_ComplaintsId",
                        column: x => x.ComplaintsId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintContent_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintContent_ContentsId",
                table: "ComplaintContent",
                column: "ContentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintContent");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ContentId",
                table: "Complaints",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Contents_ContentId",
                table: "Complaints",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class ComplaintAndComplaintBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintContent");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ComplaintBaseId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ComplaintsBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintsBase", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintBaseId",
                table: "Complaints",
                column: "ComplaintBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ContentId",
                table: "Complaints",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints",
                column: "ComplaintBaseId",
                principalTable: "ComplaintsBase",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Contents_ContentId",
                table: "Complaints",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Contents_ContentId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintsBase");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ComplaintBaseId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ContentId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ComplaintBaseId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complaints");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}

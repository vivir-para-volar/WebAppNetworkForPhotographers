using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class ComplaintNotNullForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints");

            migrationBuilder.AlterColumn<int>(
                name: "ComplaintBaseId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints",
                column: "ComplaintBaseId",
                principalTable: "ComplaintsBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints");

            migrationBuilder.AlterColumn<int>(
                name: "ComplaintBaseId",
                table: "Complaints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintsBase_ComplaintBaseId",
                table: "Complaints",
                column: "ComplaintBaseId",
                principalTable: "ComplaintsBase",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Photos",
                newName: "PhotoContent");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Photographers",
                newName: "PhotoProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoContent",
                table: "Photos",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "PhotoProfile",
                table: "Photographers",
                newName: "PhotoName");
        }
    }
}

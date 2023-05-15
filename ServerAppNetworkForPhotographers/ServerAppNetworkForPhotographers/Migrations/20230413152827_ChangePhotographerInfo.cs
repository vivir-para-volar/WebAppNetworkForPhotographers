using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class ChangePhotographerInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "PhotographersInfo");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "PhotographersInfo");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Photographers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Photographers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Photographers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Photographers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PhotographersInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PhotographersInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Photographers",
                type: "datetime2",
                nullable: true);
        }
    }
}

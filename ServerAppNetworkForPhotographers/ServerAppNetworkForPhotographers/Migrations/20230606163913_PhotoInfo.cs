using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class PhotoInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ApertureValue",
                table: "PhotosInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FocalLength",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FocalLengthIn35mmFilm",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ISOSpeedRatings",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "PhotosInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "PhotosInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XResolution",
                table: "PhotosInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YResolution",
                table: "PhotosInfo",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApertureValue",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "FocalLength",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "FocalLengthIn35mmFilm",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "ISOSpeedRatings",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "XResolution",
                table: "PhotosInfo");

            migrationBuilder.DropColumn(
                name: "YResolution",
                table: "PhotosInfo");
        }
    }
}

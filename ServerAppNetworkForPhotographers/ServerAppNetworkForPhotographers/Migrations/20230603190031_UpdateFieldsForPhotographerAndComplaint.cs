using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class UpdateFieldsForPhotographerAndComplaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Photographers");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Photographers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Open");

            migrationBuilder.AddColumn<int>(
                name: "PhotographerId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_PhotographerId",
                table: "Complaints",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Photographers_PhotographerId",
                table: "Complaints",
                column: "PhotographerId",
                principalTable: "Photographers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Photographers_PhotographerId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_PhotographerId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "PhotographerId",
                table: "Complaints");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Photographers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAppNetworkForPhotographers.Migrations
{
    public partial class FavouritesTableAndFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathPhoto",
                table: "Photos",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "PathProfilePhoto",
                table: "Photographers",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Contents",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BlogPathMainPhoto",
                table: "Contents",
                newName: "BlogMainPhotoName");

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotographerId = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_ContentId",
                table: "Favourites",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_PhotographerId",
                table: "Favourites",
                column: "PhotographerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Photos",
                newName: "PathPhoto");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Photographers",
                newName: "PathProfilePhoto");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Contents",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "BlogMainPhotoName",
                table: "Contents",
                newName: "BlogPathMainPhoto");
        }
    }
}

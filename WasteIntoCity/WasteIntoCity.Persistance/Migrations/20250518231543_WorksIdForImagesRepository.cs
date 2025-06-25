using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WorksIdForImagesRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_works_WorksId",
                table: "images");

            migrationBuilder.RenameColumn(
                name: "WorksId",
                table: "images",
                newName: "works_id");

            migrationBuilder.RenameIndex(
                name: "IX_images_WorksId",
                table: "images",
                newName: "IX_images_works_id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_works_works_id",
                table: "images",
                column: "works_id",
                principalTable: "works",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_works_works_id",
                table: "images");

            migrationBuilder.RenameColumn(
                name: "works_id",
                table: "images",
                newName: "WorksId");

            migrationBuilder.RenameIndex(
                name: "IX_images_works_id",
                table: "images",
                newName: "IX_images_WorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_works_WorksId",
                table: "images",
                column: "WorksId",
                principalTable: "works",
                principalColumn: "id");
        }
    }
}

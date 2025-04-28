using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyInUsersForImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_images_id",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_images_UsersId",
                table: "images",
                column: "UsersId",
                unique: true,
                filter: "[UsersId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_images_users_UsersId",
                table: "images",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_users_UsersId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_UsersId",
                table: "images");

            migrationBuilder.AddForeignKey(
                name: "FK_users_images_id",
                table: "users",
                column: "id",
                principalTable: "images",
                principalColumn: "id");
        }
    }
}

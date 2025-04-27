using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Image_users_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_UsersId",
                table: "images",
                column: "UsersId");

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

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "images");
        }
    }
}

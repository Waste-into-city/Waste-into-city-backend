using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ImagesUpdatedDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "uploaded_time",
                table: "images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_images_name",
                table: "images",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_images_name",
                table: "images");

            migrationBuilder.DropColumn(
                name: "uploaded_time",
                table: "images");
        }
    }
}

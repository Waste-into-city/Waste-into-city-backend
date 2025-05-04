using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WorkImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorksId",
                table: "images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_WorksId",
                table: "images",
                column: "WorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_images_works_WorksId",
                table: "images",
                column: "WorksId",
                principalTable: "works",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_works_WorksId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_WorksId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "WorksId",
                table: "images");
        }
    }
}

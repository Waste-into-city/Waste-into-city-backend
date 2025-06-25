using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WorkImagesPropertyAndWorkReportWorksId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_users_UsersId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_UsersId",
                table: "images");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "images",
                newName: "users_id");

            migrationBuilder.AddColumn<Guid>(
                name: "works_id",
                table: "work_report_results",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_work_report_results_works_id",
                table: "work_report_results",
                column: "works_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_users_id",
                table: "images",
                column: "users_id",
                unique: true,
                filter: "[users_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_images_users_users_id",
                table: "images",
                column: "users_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_report_results_works_works_id",
                table: "work_report_results",
                column: "works_id",
                principalTable: "works",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_users_users_id",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_work_report_results_works_works_id",
                table: "work_report_results");

            migrationBuilder.DropIndex(
                name: "IX_work_report_results_works_id",
                table: "work_report_results");

            migrationBuilder.DropIndex(
                name: "IX_images_users_id",
                table: "images");

            migrationBuilder.DropColumn(
                name: "works_id",
                table: "work_report_results");

            migrationBuilder.RenameColumn(
                name: "users_id",
                table: "images",
                newName: "UsersId");

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
    }
}

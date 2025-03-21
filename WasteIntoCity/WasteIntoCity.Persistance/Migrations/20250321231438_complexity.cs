using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class complexity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_work_complexity_types_work_complexities_id",
                table: "work_applications");

            migrationBuilder.RenameColumn(
                name: "work_complexities_id",
                table: "work_applications",
                newName: "work_complexity_types_id");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_work_complexities_id",
                table: "work_applications",
                newName: "IX_work_applications_work_complexity_types_id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_work_complexity_types_work_complexity_types_id",
                table: "work_applications",
                column: "work_complexity_types_id",
                principalTable: "work_complexity_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_work_complexity_types_work_complexity_types_id",
                table: "work_applications");

            migrationBuilder.RenameColumn(
                name: "work_complexity_types_id",
                table: "work_applications",
                newName: "work_complexities_id");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_work_complexity_types_id",
                table: "work_applications",
                newName: "IX_work_applications_work_complexities_id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_work_complexity_types_work_complexities_id",
                table: "work_applications",
                column: "work_complexities_id",
                principalTable: "work_complexity_types",
                principalColumn: "id");
        }
    }
}

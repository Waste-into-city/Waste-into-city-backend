using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddComplexityForWorkReportResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkComplexityTypeId",
                table: "work_report_results",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "work_complexity_types_id",
                table: "work_report_results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_work_report_results_WorkComplexityTypeId",
                table: "work_report_results",
                column: "WorkComplexityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_work_report_results_work_complexity_types_WorkComplexityTypeId",
                table: "work_report_results",
                column: "WorkComplexityTypeId",
                principalTable: "work_complexity_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_report_results_work_complexity_types_WorkComplexityTypeId",
                table: "work_report_results");

            migrationBuilder.DropIndex(
                name: "IX_work_report_results_WorkComplexityTypeId",
                table: "work_report_results");

            migrationBuilder.DropColumn(
                name: "WorkComplexityTypeId",
                table: "work_report_results");

            migrationBuilder.DropColumn(
                name: "work_complexity_types_id",
                table: "work_report_results");
        }
    }
}

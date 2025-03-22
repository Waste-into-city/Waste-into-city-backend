using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class added_status_for_work_application : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_report_complaints_work_report_complaint_status_types_work_report_complaint_status_types_id",
                table: "work_report_complaints");

            migrationBuilder.DropTable(
                name: "work_report_complaint_status_types");

            migrationBuilder.AddColumn<int>(
                name: "WorkReportStatusTypesId",
                table: "work_applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "work_report_status_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_report_status_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_WorkReportStatusTypesId",
                table: "work_applications",
                column: "WorkReportStatusTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_work_report_status_types_WorkReportStatusTypesId",
                table: "work_applications",
                column: "WorkReportStatusTypesId",
                principalTable: "work_report_status_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_report_complaints_work_report_status_types_work_report_complaint_status_types_id",
                table: "work_report_complaints",
                column: "work_report_complaint_status_types_id",
                principalTable: "work_report_status_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_work_report_status_types_WorkReportStatusTypesId",
                table: "work_applications");

            migrationBuilder.DropForeignKey(
                name: "FK_work_report_complaints_work_report_status_types_work_report_complaint_status_types_id",
                table: "work_report_complaints");

            migrationBuilder.DropTable(
                name: "work_report_status_types");

            migrationBuilder.DropIndex(
                name: "IX_work_applications_WorkReportStatusTypesId",
                table: "work_applications");

            migrationBuilder.DropColumn(
                name: "WorkReportStatusTypesId",
                table: "work_applications");

            migrationBuilder.CreateTable(
                name: "work_report_complaint_status_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_report_complaint_status_types", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_work_report_complaints_work_report_complaint_status_types_work_report_complaint_status_types_id",
                table: "work_report_complaints",
                column: "work_report_complaint_status_types_id",
                principalTable: "work_report_complaint_status_types",
                principalColumn: "id");
        }
    }
}

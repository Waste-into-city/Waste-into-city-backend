using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class added_fields_from_user_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_users_FromUsersId",
                table: "work_applications");

            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_work_report_status_types_WorkReportStatusTypesId",
                table: "work_applications");

            migrationBuilder.RenameColumn(
                name: "WorkReportStatusTypesId",
                table: "work_applications",
                newName: "work_report_status_types_id");

            migrationBuilder.RenameColumn(
                name: "FromUsersId",
                table: "work_applications",
                newName: "from_users_id");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_WorkReportStatusTypesId",
                table: "work_applications",
                newName: "IX_work_applications_work_report_status_types_id");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_FromUsersId",
                table: "work_applications",
                newName: "IX_work_applications_from_users_id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_users_from_users_id",
                table: "work_applications",
                column: "from_users_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_work_report_status_types_work_report_status_types_id",
                table: "work_applications",
                column: "work_report_status_types_id",
                principalTable: "work_report_status_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_users_from_users_id",
                table: "work_applications");

            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_work_report_status_types_work_report_status_types_id",
                table: "work_applications");

            migrationBuilder.RenameColumn(
                name: "work_report_status_types_id",
                table: "work_applications",
                newName: "WorkReportStatusTypesId");

            migrationBuilder.RenameColumn(
                name: "from_users_id",
                table: "work_applications",
                newName: "FromUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_work_report_status_types_id",
                table: "work_applications",
                newName: "IX_work_applications_WorkReportStatusTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_work_applications_from_users_id",
                table: "work_applications",
                newName: "IX_work_applications_FromUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_users_FromUsersId",
                table: "work_applications",
                column: "FromUsersId",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_work_report_status_types_WorkReportStatusTypesId",
                table: "work_applications",
                column: "WorkReportStatusTypesId",
                principalTable: "work_report_status_types",
                principalColumn: "id");
        }
    }
}

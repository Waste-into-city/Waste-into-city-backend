using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class add_coordinates_report_complexity_type_start_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trashcan_point_reports_trashcan_points_trashcan_points_id",
                table: "trashcan_point_reports");

            migrationBuilder.DropForeignKey(
                name: "FK_trashcans_trashcan_points_trashcan_points_id",
                table: "trashcans");

            migrationBuilder.DropTable(
                name: "trashcan_points");

            migrationBuilder.DropIndex(
                name: "IX_trashcans_trashcan_points_id",
                table: "trashcans");

            migrationBuilder.DropIndex(
                name: "IX_trashcan_point_reports_trashcan_points_id",
                table: "trashcan_point_reports");

            migrationBuilder.DropColumn(
                name: "trashcan_points_id",
                table: "trashcans");

            migrationBuilder.DropColumn(
                name: "trashcan_points_id",
                table: "trashcan_point_reports");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatesId",
                table: "works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "started_datetime",
                table: "work_report_complaints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "work_report_complaint_types_id",
                table: "work_report_complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "coordinates_id",
                table: "work_applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "started_datetime",
                table: "work_applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "coordinates_id",
                table: "trashcans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "coordinates_id",
                table: "trashcan_point_reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "coordinates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lng = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_report_complaint_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_report_complaint_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_works_CoordinatesId",
                table: "works",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_complaints_work_report_complaint_types_id",
                table: "work_report_complaints",
                column: "work_report_complaint_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_coordinates_id",
                table: "work_applications",
                column: "coordinates_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcans_coordinates_id",
                table: "trashcans",
                column: "coordinates_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_reports_coordinates_id",
                table: "trashcan_point_reports",
                column: "coordinates_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trashcan_point_reports_coordinates_coordinates_id",
                table: "trashcan_point_reports",
                column: "coordinates_id",
                principalTable: "coordinates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_trashcans_coordinates_coordinates_id",
                table: "trashcans",
                column: "coordinates_id",
                principalTable: "coordinates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_applications_coordinates_coordinates_id",
                table: "work_applications",
                column: "coordinates_id",
                principalTable: "coordinates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_report_complaints_work_report_complaint_types_work_report_complaint_types_id",
                table: "work_report_complaints",
                column: "work_report_complaint_types_id",
                principalTable: "work_report_complaint_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_works_coordinates_CoordinatesId",
                table: "works",
                column: "CoordinatesId",
                principalTable: "coordinates",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trashcan_point_reports_coordinates_coordinates_id",
                table: "trashcan_point_reports");

            migrationBuilder.DropForeignKey(
                name: "FK_trashcans_coordinates_coordinates_id",
                table: "trashcans");

            migrationBuilder.DropForeignKey(
                name: "FK_work_applications_coordinates_coordinates_id",
                table: "work_applications");

            migrationBuilder.DropForeignKey(
                name: "FK_work_report_complaints_work_report_complaint_types_work_report_complaint_types_id",
                table: "work_report_complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_works_coordinates_CoordinatesId",
                table: "works");

            migrationBuilder.DropTable(
                name: "coordinates");

            migrationBuilder.DropTable(
                name: "work_report_complaint_types");

            migrationBuilder.DropIndex(
                name: "IX_works_CoordinatesId",
                table: "works");

            migrationBuilder.DropIndex(
                name: "IX_work_report_complaints_work_report_complaint_types_id",
                table: "work_report_complaints");

            migrationBuilder.DropIndex(
                name: "IX_work_applications_coordinates_id",
                table: "work_applications");

            migrationBuilder.DropIndex(
                name: "IX_trashcans_coordinates_id",
                table: "trashcans");

            migrationBuilder.DropIndex(
                name: "IX_trashcan_point_reports_coordinates_id",
                table: "trashcan_point_reports");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "works");

            migrationBuilder.DropColumn(
                name: "started_datetime",
                table: "work_report_complaints");

            migrationBuilder.DropColumn(
                name: "work_report_complaint_types_id",
                table: "work_report_complaints");

            migrationBuilder.DropColumn(
                name: "coordinates_id",
                table: "work_applications");

            migrationBuilder.DropColumn(
                name: "started_datetime",
                table: "work_applications");

            migrationBuilder.DropColumn(
                name: "coordinates_id",
                table: "trashcans");

            migrationBuilder.DropColumn(
                name: "coordinates_id",
                table: "trashcan_point_reports");

            migrationBuilder.AddColumn<Guid>(
                name: "trashcan_points_id",
                table: "trashcans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "trashcan_points_id",
                table: "trashcan_point_reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "trashcan_points",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lng = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcan_points", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trashcans_trashcan_points_id",
                table: "trashcans",
                column: "trashcan_points_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_reports_trashcan_points_id",
                table: "trashcan_point_reports",
                column: "trashcan_points_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trashcan_point_reports_trashcan_points_trashcan_points_id",
                table: "trashcan_point_reports",
                column: "trashcan_points_id",
                principalTable: "trashcan_points",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_trashcans_trashcan_points_trashcan_points_id",
                table: "trashcans",
                column: "trashcan_points_id",
                principalTable: "trashcan_points",
                principalColumn: "id");
        }
    }
}

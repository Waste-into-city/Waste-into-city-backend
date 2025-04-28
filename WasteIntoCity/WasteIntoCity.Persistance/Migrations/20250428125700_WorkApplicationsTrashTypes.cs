using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WorkApplicationsTrashTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkTypesId",
                table: "work_applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "work_applications_trash_types",
                columns: table => new
                {
                    work_applications = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trash_types_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_applications_trash_types", x => new { x.work_applications, x.trash_types_id });
                    table.ForeignKey(
                        name: "FK_work_applications_trash_types_trash_types_trash_types_id",
                        column: x => x.trash_types_id,
                        principalTable: "trash_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_applications_trash_types_work_applications_work_applications",
                        column: x => x.work_applications,
                        principalTable: "work_applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_trash_types_trash_types_id",
                table: "work_applications_trash_types",
                column: "trash_types_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "work_applications_trash_types");

            migrationBuilder.DropColumn(
                name: "WorkTypesId",
                table: "work_applications");
        }
    }
}

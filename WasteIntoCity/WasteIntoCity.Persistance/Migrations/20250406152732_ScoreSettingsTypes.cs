using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ScoreSettingsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_settings");

            migrationBuilder.CreateTable(
                name: "score_settings_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_score_settings_types", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "score_settings_types");

            migrationBuilder.CreateTable(
                name: "admin_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    acceptable_difference_report_trashcan_occupancy = table.Column<int>(type: "int", nullable: false),
                    false_complaint_from_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    false_report_trashcans_occupancy_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    true_complaint_from_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    true_complaint_to_addition_ranking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_settings", x => x.id);
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class WorkColleagueReportsAddUniqueIndexForColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_work_colleague_reports_from_participant_id",
                table: "work_colleague_reports");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_from_participant_id_about_colleague_id_works_id",
                table: "work_colleague_reports",
                columns: new[] { "from_participant_id", "about_colleague_id", "works_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_work_colleague_reports_from_participant_id_about_colleague_id_works_id",
                table: "work_colleague_reports");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_from_participant_id",
                table: "work_colleague_reports",
                column: "from_participant_id");
        }
    }
}

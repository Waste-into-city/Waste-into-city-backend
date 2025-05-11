using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFromApplicationFieldWorkTypesAndChangedPrecisionOfCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkTypesId",
                table: "work_applications");

            migrationBuilder.AlterColumn<decimal>(
                name: "lng",
                table: "coordinates",
                type: "decimal(20,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "lat",
                table: "coordinates",
                type: "decimal(20,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,9)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkTypesId",
                table: "work_applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "lng",
                table: "coordinates",
                type: "decimal(12,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,17)");

            migrationBuilder.AlterColumn<decimal>(
                name: "lat",
                table: "coordinates",
                type: "decimal(12,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,17)");
        }
    }
}

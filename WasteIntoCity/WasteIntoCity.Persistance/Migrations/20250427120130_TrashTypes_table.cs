using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class TrashTypes_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trash_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trash_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "works_trash_types",
                columns: table => new
                {
                    works_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trash_types_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_trash_types", x => new { x.works_id, x.trash_types_id });
                    table.ForeignKey(
                        name: "FK_works_trash_types_trash_types_trash_types_id",
                        column: x => x.trash_types_id,
                        principalTable: "trash_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_works_trash_types_works_works_id",
                        column: x => x.works_id,
                        principalTable: "works",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_trash_types_name",
                table: "trash_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_works_trash_types_trash_types_id",
                table: "works_trash_types",
                column: "trash_types_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "works_trash_types");

            migrationBuilder.DropTable(
                name: "trash_types");
        }
    }
}

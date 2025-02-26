using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class tokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "access_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    expiration_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_tokens", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_access_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    expiration_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_tokens");

            migrationBuilder.DropTable(
                name: "refresh_tokens");
        }
    }
}

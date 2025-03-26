using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteIntoCity.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    true_complaint_to_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    false_complaint_from_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    true_complaint_from_addition_ranking = table.Column<int>(type: "int", nullable: false),
                    acceptable_difference_report_trashcan_occupancy = table.Column<int>(type: "int", nullable: false),
                    false_report_trashcans_occupancy_addition_ranking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coordinates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lng = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trashcan_occupancy_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcan_occupancy_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trashcan_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcan_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ranking = table.Column<int>(type: "int", nullable: false),
                    negative_score = table.Column<int>(type: "int", nullable: false),
                    is_banned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_complexity_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    participants_min = table.Column<int>(type: "int", nullable: false),
                    participants_max = table.Column<int>(type: "int", nullable: false),
                    duration_hours = table.Column<int>(type: "int", nullable: false),
                    multiplier_ranking = table.Column<int>(type: "int", nullable: false),
                    radius_on_map = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_complexity_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_mark_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    addition_ranking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_mark_types", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "work_status_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    multiplier_ranking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_status_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trashcans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volume = table.Column<int>(type: "int", nullable: false),
                    coordinates_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trashcan_types_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    average_trashcan_occupancy_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcans", x => x.id);
                    table.ForeignKey(
                        name: "FK_trashcans_coordinates_coordinates_id",
                        column: x => x.coordinates_id,
                        principalTable: "coordinates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_trashcans_trashcan_occupancy_types_average_trashcan_occupancy_type_id",
                        column: x => x.average_trashcan_occupancy_type_id,
                        principalTable: "trashcan_occupancy_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_trashcans_trashcan_types_trashcan_types_id",
                        column: x => x.trashcan_types_id,
                        principalTable: "trashcan_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    from_users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    to_users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_users_from_users_id",
                        column: x => x.from_users_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_notifications_users_to_users_id",
                        column: x => x.to_users_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    value = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    jwt_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    creation_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiration_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    used = table.Column<bool>(type: "bit", nullable: false),
                    invalidated = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.value);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trashcan_point_reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_required = table.Column<bool>(type: "bit", nullable: false),
                    users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    coordinates_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    submission_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcan_point_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_trashcan_point_reports_coordinates_coordinates_id",
                        column: x => x.coordinates_id,
                        principalTable: "coordinates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_trashcan_point_reports_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_according_roles",
                columns: table => new
                {
                    users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roles_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_according_roles", x => new { x.roles_id, x.users_id });
                    table.ForeignKey(
                        name: "FK_user_according_roles_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_according_roles_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_applications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    started_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    work_complexity_types_id = table.Column<int>(type: "int", nullable: false),
                    work_report_status_types_id = table.Column<int>(type: "int", nullable: false),
                    coordinates_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from_users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_applications_coordinates_coordinates_id",
                        column: x => x.coordinates_id,
                        principalTable: "coordinates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_applications_users_from_users_id",
                        column: x => x.from_users_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_applications_work_complexity_types_work_complexity_types_id",
                        column: x => x.work_complexity_types_id,
                        principalTable: "work_complexity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_applications_work_report_status_types_work_report_status_types_id",
                        column: x => x.work_report_status_types_id,
                        principalTable: "work_report_status_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_report_results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    from_participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    work_statuses_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_report_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_work_report_results_users_from_participant_id",
                        column: x => x.from_participant_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_report_results_work_status_types_work_statuses_id",
                        column: x => x.work_statuses_id,
                        principalTable: "work_status_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    start_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finish_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    work_complexity_id = table.Column<int>(type: "int", nullable: false),
                    work_statuses_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordinatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.id);
                    table.ForeignKey(
                        name: "FK_works_coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "coordinates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_works_work_complexity_types_work_complexity_id",
                        column: x => x.work_complexity_id,
                        principalTable: "work_complexity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_works_work_status_types_work_statuses_id",
                        column: x => x.work_statuses_id,
                        principalTable: "work_status_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trashcan_point_report_each_mark",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trashcan_point_reports_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trashcans_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trashcan_occupancy_types_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trashcan_point_report_each_mark", x => x.id);
                    table.ForeignKey(
                        name: "FK_trashcan_point_report_each_mark_trashcan_occupancy_types_trashcan_occupancy_types_id",
                        column: x => x.trashcan_occupancy_types_id,
                        principalTable: "trashcan_occupancy_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_trashcan_point_report_each_mark_trashcan_point_reports_trashcan_point_reports_id",
                        column: x => x.trashcan_point_reports_id,
                        principalTable: "trashcan_point_reports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_trashcan_point_report_each_mark_trashcans_trashcans_id",
                        column: x => x.trashcans_id,
                        principalTable: "trashcans",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_colleague_reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from_participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    about_colleague_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    works_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    work_mark_types_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_colleague_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_colleague_reports_users_about_colleague_id",
                        column: x => x.about_colleague_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_colleague_reports_users_from_participant_id",
                        column: x => x.from_participant_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_colleague_reports_work_mark_types_work_mark_types_id",
                        column: x => x.work_mark_types_id,
                        principalTable: "work_mark_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_colleague_reports_works_works_id",
                        column: x => x.works_id,
                        principalTable: "works",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_participants",
                columns: table => new
                {
                    works_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    participants_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_participants", x => new { x.works_id, x.participants_id });
                    table.ForeignKey(
                        name: "FK_work_participants_users_participants_id",
                        column: x => x.participants_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_participants_works_works_id",
                        column: x => x.works_id,
                        principalTable: "works",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_report_complaints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    started_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    works_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from_users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    work_report_complaint_status_types_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_report_complaints", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_report_complaints_users_from_users_id",
                        column: x => x.from_users_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_report_complaints_work_report_status_types_work_report_complaint_status_types_id",
                        column: x => x.work_report_complaint_status_types_id,
                        principalTable: "work_report_status_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_work_report_complaints_works_works_id",
                        column: x => x.works_id,
                        principalTable: "works",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    work_applications_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    workReport_complaints_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    work_report_results_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_work_applications_work_applications_id",
                        column: x => x.work_applications_id,
                        principalTable: "work_applications",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_images_work_report_complaints_workReport_complaints_id",
                        column: x => x.workReport_complaints_id,
                        principalTable: "work_report_complaints",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_images_work_report_results_work_report_results_id",
                        column: x => x.work_report_results_id,
                        principalTable: "work_report_results",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_work_applications_id",
                table: "images",
                column: "work_applications_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_work_report_results_id",
                table: "images",
                column: "work_report_results_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_workReport_complaints_id",
                table: "images",
                column: "workReport_complaints_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_from_users_id",
                table: "notifications",
                column: "from_users_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_to_users_id",
                table: "notifications",
                column: "to_users_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_occupancy_types_name",
                table: "trashcan_occupancy_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_report_each_mark_trashcan_occupancy_types_id",
                table: "trashcan_point_report_each_mark",
                column: "trashcan_occupancy_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_report_each_mark_trashcan_point_reports_id",
                table: "trashcan_point_report_each_mark",
                column: "trashcan_point_reports_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_report_each_mark_trashcans_id",
                table: "trashcan_point_report_each_mark",
                column: "trashcans_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_reports_coordinates_id",
                table: "trashcan_point_reports",
                column: "coordinates_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_point_reports_users_id",
                table: "trashcan_point_reports",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcan_types_name",
                table: "trashcan_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trashcans_average_trashcan_occupancy_type_id",
                table: "trashcans",
                column: "average_trashcan_occupancy_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcans_coordinates_id",
                table: "trashcans",
                column: "coordinates_id");

            migrationBuilder.CreateIndex(
                name: "IX_trashcans_trashcan_types_id",
                table: "trashcans",
                column: "trashcan_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_according_roles_users_id",
                table: "user_according_roles",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_coordinates_id",
                table: "work_applications",
                column: "coordinates_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_from_users_id",
                table: "work_applications",
                column: "from_users_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_work_complexity_types_id",
                table: "work_applications",
                column: "work_complexity_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_applications_work_report_status_types_id",
                table: "work_applications",
                column: "work_report_status_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_about_colleague_id",
                table: "work_colleague_reports",
                column: "about_colleague_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_from_participant_id",
                table: "work_colleague_reports",
                column: "from_participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_work_mark_types_id",
                table: "work_colleague_reports",
                column: "work_mark_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_colleague_reports_works_id",
                table: "work_colleague_reports",
                column: "works_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_complexity_types_name",
                table: "work_complexity_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_mark_types_name",
                table: "work_mark_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_participants_participants_id",
                table: "work_participants",
                column: "participants_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_complaints_from_users_id",
                table: "work_report_complaints",
                column: "from_users_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_complaints_work_report_complaint_status_types_id",
                table: "work_report_complaints",
                column: "work_report_complaint_status_types_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_complaints_works_id",
                table: "work_report_complaints",
                column: "works_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_results_from_participant_id",
                table: "work_report_results",
                column: "from_participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_report_results_work_statuses_id",
                table: "work_report_results",
                column: "work_statuses_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_status_types_name",
                table: "work_status_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_works_CoordinatesId",
                table: "works",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_works_work_complexity_id",
                table: "works",
                column: "work_complexity_id");

            migrationBuilder.CreateIndex(
                name: "IX_works_work_statuses_id",
                table: "works",
                column: "work_statuses_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_settings");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "trashcan_point_report_each_mark");

            migrationBuilder.DropTable(
                name: "user_according_roles");

            migrationBuilder.DropTable(
                name: "work_colleague_reports");

            migrationBuilder.DropTable(
                name: "work_participants");

            migrationBuilder.DropTable(
                name: "work_applications");

            migrationBuilder.DropTable(
                name: "work_report_complaints");

            migrationBuilder.DropTable(
                name: "work_report_results");

            migrationBuilder.DropTable(
                name: "trashcan_point_reports");

            migrationBuilder.DropTable(
                name: "trashcans");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "work_mark_types");

            migrationBuilder.DropTable(
                name: "work_report_status_types");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "trashcan_occupancy_types");

            migrationBuilder.DropTable(
                name: "trashcan_types");

            migrationBuilder.DropTable(
                name: "coordinates");

            migrationBuilder.DropTable(
                name: "work_complexity_types");

            migrationBuilder.DropTable(
                name: "work_status_types");
        }
    }
}

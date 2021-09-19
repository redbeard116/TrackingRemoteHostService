using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TrackingRemoteHostService.Migrations
{
    public partial class AddHostsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hosts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hosts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shedules",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hostid = table.Column<int>(type: "integer", nullable: false),
                    interval = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shedules", x => x.id);
                    table.ForeignKey(
                        name: "FK_shedules_hosts_hostid",
                        column: x => x.hostid,
                        principalSchema: "public",
                        principalTable: "hosts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userschedule",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    scheduleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userschedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_userschedule_shedules_scheduleId",
                        column: x => x.scheduleId,
                        principalSchema: "public",
                        principalTable: "shedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userschedule_users_userid",
                        column: x => x.userid,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hosts_url",
                schema: "public",
                table: "hosts",
                column: "url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shedules_hostid",
                schema: "public",
                table: "shedules",
                column: "hostid");

            migrationBuilder.CreateIndex(
                name: "IX_userschedule_scheduleId",
                schema: "public",
                table: "userschedule",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_userschedule_userid",
                schema: "public",
                table: "userschedule",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userschedule",
                schema: "public");

            migrationBuilder.DropTable(
                name: "shedules",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hosts",
                schema: "public");
        }
    }
}

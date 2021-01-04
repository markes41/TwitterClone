using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    SeguidoresMail = table.Column<string>(type: "TEXT", nullable: false),
                    SeguidosMail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.SeguidoresMail, x.SeguidosMail });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_SeguidoresMail",
                        column: x => x.SeguidoresMail,
                        principalTable: "Users",
                        principalColumn: "Mail",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_SeguidosMail",
                        column: x => x.SeguidosMail,
                        principalTable: "Users",
                        principalColumn: "Mail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_SeguidosMail",
                table: "UserUser",
                column: "SeguidosMail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");
        }
    }
}

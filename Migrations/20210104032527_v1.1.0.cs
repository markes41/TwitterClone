using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SeguidoresMail",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SeguidosMail",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "SeguidosMail",
                table: "UserUser",
                newName: "FollowsMail");

            migrationBuilder.RenameColumn(
                name: "SeguidoresMail",
                table: "UserUser",
                newName: "FollowersMail");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_SeguidosMail",
                table: "UserUser",
                newName: "IX_UserUser_FollowsMail");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_FollowersMail",
                table: "UserUser",
                column: "FollowersMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_FollowsMail",
                table: "UserUser",
                column: "FollowsMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_FollowersMail",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_FollowsMail",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "FollowsMail",
                table: "UserUser",
                newName: "SeguidosMail");

            migrationBuilder.RenameColumn(
                name: "FollowersMail",
                table: "UserUser",
                newName: "SeguidoresMail");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_FollowsMail",
                table: "UserUser",
                newName: "IX_UserUser_SeguidosMail");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SeguidoresMail",
                table: "UserUser",
                column: "SeguidoresMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SeguidosMail",
                table: "UserUser",
                column: "SeguidosMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

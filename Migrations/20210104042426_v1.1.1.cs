using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_FollowsMail",
                table: "UserUser");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "FollowsMail",
                table: "UserUser",
                newName: "FollowingMail");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_FollowsMail",
                table: "UserUser",
                newName: "IX_UserUser_FollowingMail");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_FollowingMail",
                table: "UserUser",
                column: "FollowingMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_FollowingMail",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "FollowingMail",
                table: "UserUser",
                newName: "FollowsMail");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_FollowingMail",
                table: "UserUser",
                newName: "IX_UserUser_FollowsMail");

            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "Tweets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_FollowsMail",
                table: "UserUser",
                column: "FollowsMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

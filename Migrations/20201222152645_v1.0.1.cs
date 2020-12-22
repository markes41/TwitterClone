using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweet_Users_UserMail",
                table: "Tweet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweet",
                table: "Tweet");

            migrationBuilder.RenameTable(
                name: "Tweet",
                newName: "Tweets");

            migrationBuilder.RenameIndex(
                name: "IX_Tweet_UserMail",
                table: "Tweets",
                newName: "IX_Tweets_UserMail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "TweetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserMail",
                table: "Tweets",
                column: "UserMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserMail",
                table: "Tweets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.RenameTable(
                name: "Tweets",
                newName: "Tweet");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserMail",
                table: "Tweet",
                newName: "IX_Tweet_UserMail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweet",
                table: "Tweet",
                column: "TweetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweet_Users_UserMail",
                table: "Tweet",
                column: "UserMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserCreatorContact",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Users",
                newName: "Mail");

            migrationBuilder.RenameColumn(
                name: "UserCreatorContact",
                table: "Tweets",
                newName: "UserCreatorMail");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserCreatorContact",
                table: "Tweets",
                newName: "IX_Tweets_UserCreatorMail");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserCreatorMail",
                table: "Tweets",
                column: "UserCreatorMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserCreatorMail",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Users",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "UserCreatorMail",
                table: "Tweets",
                newName: "UserCreatorContact");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserCreatorMail",
                table: "Tweets",
                newName: "IX_Tweets_UserCreatorContact");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserCreatorContact",
                table: "Tweets",
                column: "UserCreatorContact",
                principalTable: "Users",
                principalColumn: "Contact",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

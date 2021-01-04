using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_OwnerMail",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "OwnerMail",
                table: "Tweets",
                newName: "UserMail");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_OwnerMail",
                table: "Tweets",
                newName: "IX_Tweets_UserMail");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Tweets",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "UserMail",
                table: "Tweets",
                newName: "OwnerMail");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserMail",
                table: "Tweets",
                newName: "IX_Tweets_OwnerMail");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_OwnerMail",
                table: "Tweets",
                column: "OwnerMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

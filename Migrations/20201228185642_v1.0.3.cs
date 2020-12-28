using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserMail",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Users",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "UserMail",
                table: "Tweets",
                newName: "UserContact");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserMail",
                table: "Tweets",
                newName: "IX_Tweets_UserContact");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserContact",
                table: "Tweets",
                column: "UserContact",
                principalTable: "Users",
                principalColumn: "Contact",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserContact",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Users",
                newName: "Mail");

            migrationBuilder.RenameColumn(
                name: "UserContact",
                table: "Tweets",
                newName: "UserMail");

            migrationBuilder.RenameIndex(
                name: "IX_Tweets_UserContact",
                table: "Tweets",
                newName: "IX_Tweets_UserMail");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserMail",
                table: "Tweets",
                column: "UserMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

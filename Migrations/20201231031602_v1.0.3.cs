using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetUser");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Retweet",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Tweets",
                newName: "OwnerMail");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_OwnerMail",
                table: "Tweets",
                column: "OwnerMail");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_OwnerMail",
                table: "Tweets",
                column: "OwnerMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_OwnerMail",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_OwnerMail",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "OwnerMail",
                table: "Tweets",
                newName: "ProfilePicture");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Tweets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Tweets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Retweet",
                table: "Tweets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TweetUser",
                columns: table => new
                {
                    TweetsTweetID = table.Column<long>(type: "INTEGER", nullable: false),
                    UserCreatorMail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetUser", x => new { x.TweetsTweetID, x.UserCreatorMail });
                    table.ForeignKey(
                        name: "FK_TweetUser_Tweets_TweetsTweetID",
                        column: x => x.TweetsTweetID,
                        principalTable: "Tweets",
                        principalColumn: "TweetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetUser_Users_UserCreatorMail",
                        column: x => x.UserCreatorMail,
                        principalTable: "Users",
                        principalColumn: "Mail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TweetUser_UserCreatorMail",
                table: "TweetUser",
                column: "UserCreatorMail");
        }
    }
}

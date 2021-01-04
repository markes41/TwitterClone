using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UserCreatorMail",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_UserCreatorMail",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "UserCreatorMail",
                table: "Tweets");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetUser");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatorMail",
                table: "Tweets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UserCreatorMail",
                table: "Tweets",
                column: "UserCreatorMail");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UserCreatorMail",
                table: "Tweets",
                column: "UserCreatorMail",
                principalTable: "Users",
                principalColumn: "Mail",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

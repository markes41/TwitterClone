using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v108 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Retweet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    ToLikeMail = table.Column<string>(type: "TEXT", nullable: true),
                    TweetID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK_Like_Tweets_TweetID",
                        column: x => x.TweetID,
                        principalTable: "Tweets",
                        principalColumn: "TweetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Like_Users_ToLikeMail",
                        column: x => x.ToLikeMail,
                        principalTable: "Users",
                        principalColumn: "Mail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Retweet",
                columns: table => new
                {
                    RetweetID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    ToRetweetMail = table.Column<string>(type: "TEXT", nullable: true),
                    TweetID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retweet", x => x.RetweetID);
                    table.ForeignKey(
                        name: "FK_Retweet_Tweets_TweetID",
                        column: x => x.TweetID,
                        principalTable: "Tweets",
                        principalColumn: "TweetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Retweet_Users_ToRetweetMail",
                        column: x => x.ToRetweetMail,
                        principalTable: "Users",
                        principalColumn: "Mail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_ToLikeMail",
                table: "Like",
                column: "ToLikeMail");

            migrationBuilder.CreateIndex(
                name: "IX_Like_TweetID",
                table: "Like",
                column: "TweetID");

            migrationBuilder.CreateIndex(
                name: "IX_Retweet_ToRetweetMail",
                table: "Retweet",
                column: "ToRetweetMail");

            migrationBuilder.CreateIndex(
                name: "IX_Retweet_TweetID",
                table: "Retweet",
                column: "TweetID");
        }
    }
}

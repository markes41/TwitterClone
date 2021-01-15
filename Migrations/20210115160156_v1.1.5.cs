using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TweetID1",
                table: "Tweets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_TweetID1",
                table: "Tweets",
                column: "TweetID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Tweets_TweetID1",
                table: "Tweets",
                column: "TweetID1",
                principalTable: "Tweets",
                principalColumn: "TweetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Tweets_TweetID1",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_TweetID1",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TweetID1",
                table: "Tweets");
        }
    }
}

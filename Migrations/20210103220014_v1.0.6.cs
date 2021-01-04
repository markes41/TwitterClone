using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "Tweets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Tweets");
        }
    }
}

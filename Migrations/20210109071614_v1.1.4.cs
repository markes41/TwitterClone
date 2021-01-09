using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterClone.Migrations
{
    public partial class v114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationDay",
                table: "Tweets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationMonth",
                table: "Tweets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationYear",
                table: "Tweets",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDay",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "CreationMonth",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "CreationYear",
                table: "Tweets");
        }
    }
}

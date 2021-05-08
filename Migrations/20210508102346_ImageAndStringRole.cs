using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieUserManagerService.Migrations
{
    public partial class ImageAndStringRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

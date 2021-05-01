using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieUserManagerService.Migrations
{
    public partial class LastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "lastname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "name");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class photolist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class CustomerReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "Location", "Price" },
                values: new object[] { 200, "Mdint nasr", 200000 });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "Location", "Price" },
                values: new object[] { 200, "Mdint nasr", 200000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Employees");
        }
    }
}

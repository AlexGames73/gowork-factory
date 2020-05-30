using Microsoft.EntityFrameworkCore.Migrations;

namespace GoWorkFactoryDataBase.Migrations
{
    public partial class addCountProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialAmount",
                table: "MaterialRequests");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MaterialAmount",
                table: "MaterialRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

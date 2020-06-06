using Microsoft.EntityFrameworkCore.Migrations;

namespace GoWorkFactoryDataBase.Migrations
{
    public partial class OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Orders",
                type: "text",
                nullable: true);
        }
    }
}

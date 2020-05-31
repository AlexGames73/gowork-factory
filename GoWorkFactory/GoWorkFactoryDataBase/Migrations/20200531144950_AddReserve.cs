using Microsoft.EntityFrameworkCore.Migrations;

namespace GoWorkFactoryDataBase.Migrations
{
    public partial class AddReserve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserve",
                table: "MaterialProducts");

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserve",
                table: "MaterialProducts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

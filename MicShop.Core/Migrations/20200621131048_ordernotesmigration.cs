using Microsoft.EntityFrameworkCore.Migrations;

namespace MicShop.Core.Migrations
{
    public partial class ordernotesmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNotes",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNotes",
                table: "Order");
        }
    }
}

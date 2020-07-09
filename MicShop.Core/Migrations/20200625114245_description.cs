using Microsoft.EntityFrameworkCore.Migrations;

namespace MicShop.Core.Migrations
{
    public partial class description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

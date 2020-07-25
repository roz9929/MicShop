using Microsoft.EntityFrameworkCore.Migrations;

namespace MicShop.Core.Migrations
{
    public partial class imagemig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

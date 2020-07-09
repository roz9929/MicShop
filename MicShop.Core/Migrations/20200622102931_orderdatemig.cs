using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicShop.Core.Migrations
{
    public partial class orderdatemig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Order");
        }
    }
}

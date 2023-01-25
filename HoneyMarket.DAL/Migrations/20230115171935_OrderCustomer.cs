using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoneyMarket.DAL.Migrations
{
    public partial class OrderCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerOrders",
                newName: "CustomerEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "JsonId",
                table: "CustomerOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonId",
                table: "CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "CustomerOrders",
                newName: "CustomerId");
        }
    }
}

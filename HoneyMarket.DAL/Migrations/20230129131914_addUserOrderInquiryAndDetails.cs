using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoneyMarket.DAL.Migrations
{
    public partial class addUserOrderInquiryAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOrderInquiryHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InquiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderInquiryHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrderInquiryHeader_AspNetUsers_ShopUserId",
                        column: x => x.ShopUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOrderInquiryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserOrderInquiryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderInquiryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrderInquiryDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrderInquiryDetails_UserOrderInquiryHeader_UserOrderInquiryId",
                        column: x => x.UserOrderInquiryId,
                        principalTable: "UserOrderInquiryHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderInquiryDetails_ProductId",
                table: "UserOrderInquiryDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderInquiryDetails_UserOrderInquiryId",
                table: "UserOrderInquiryDetails",
                column: "UserOrderInquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderInquiryHeader_ShopUserId",
                table: "UserOrderInquiryHeader",
                column: "ShopUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrderInquiryDetails");

            migrationBuilder.DropTable(
                name: "UserOrderInquiryHeader");
        }
    }
}

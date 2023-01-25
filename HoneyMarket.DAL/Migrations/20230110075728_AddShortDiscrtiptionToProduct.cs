using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoneyMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShortDiscrtiptionToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDiscription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDiscription",
                table: "Products");
        }
    }
}

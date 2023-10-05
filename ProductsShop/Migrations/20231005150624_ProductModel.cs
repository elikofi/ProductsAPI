using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsShop.Migrations
{
    /// <inheritdoc />
    public partial class ProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

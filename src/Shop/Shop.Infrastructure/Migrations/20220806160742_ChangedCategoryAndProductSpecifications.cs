using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedCategoryAndProductSpecifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "category",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "IsImportantFeature",
                schema: "product",
                table: "CustomSpecifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "category",
                table: "Specifications",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsImportantFeature",
                schema: "product",
                table: "CustomSpecifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

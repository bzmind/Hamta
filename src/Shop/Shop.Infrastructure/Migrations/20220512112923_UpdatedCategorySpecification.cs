using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class UpdatedCategorySpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "product",
                table: "CustomSpecifications",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "product",
                table: "CustomSpecifications",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "IsImportantFeature",
                schema: "category",
                table: "Specifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImportantFeature",
                schema: "category",
                table: "Specifications");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "product",
                table: "CustomSpecifications",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "product",
                table: "CustomSpecifications",
                newName: "Value");
        }
    }
}

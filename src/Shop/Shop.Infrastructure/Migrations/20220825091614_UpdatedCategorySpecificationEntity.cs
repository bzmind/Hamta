using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class UpdatedCategorySpecificationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsImportantFeature",
                schema: "category",
                table: "Specifications",
                newName: "IsOptional");

            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                schema: "category",
                table: "Specifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImportant",
                schema: "category",
                table: "Specifications");

            migrationBuilder.RenameColumn(
                name: "IsOptional",
                schema: "category",
                table: "Specifications",
                newName: "IsImportantFeature");
        }
    }
}

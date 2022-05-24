using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedScoreToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Value",
                schema: "product",
                table: "Scores",
                type: "float(2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                schema: "product",
                table: "Scores",
                type: "int",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float(2)");
        }
    }
}

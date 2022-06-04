using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class FixTokensTableSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Tokens",
                schema: "token",
                newName: "Tokens",
                newSchema: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "token");

            migrationBuilder.RenameTable(
                name: "Tokens",
                schema: "user",
                newName: "Tokens",
                newSchema: "token");
        }
    }
}

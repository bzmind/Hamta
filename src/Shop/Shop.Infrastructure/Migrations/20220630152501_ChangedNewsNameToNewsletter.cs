using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedNewsNameToNewsletter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSubscribedToNews",
                schema: "user",
                table: "Users",
                newName: "IsSubscribedToNewsletter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSubscribedToNewsletter",
                schema: "user",
                table: "Users",
                newName: "IsSubscribedToNews");
        }
    }
}

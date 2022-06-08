using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class AddFKConstraintToUserRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE [user].Roles
                ADD CONSTRAINT FK_Roles_Roles_RoleId
                FOREIGN KEY (RoleId) REFERENCES [role].Roles(Id);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

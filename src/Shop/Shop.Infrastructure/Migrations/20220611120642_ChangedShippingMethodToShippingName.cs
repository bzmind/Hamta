using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedShippingMethodToShippingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Method",
                schema: "shipping",
                table: "Shippings",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShippingMethod",
                schema: "order",
                table: "Orders",
                newName: "ShippingName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "shipping",
                table: "Shippings",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "ShippingName",
                schema: "order",
                table: "Orders",
                newName: "ShippingMethod");
        }
    }
}

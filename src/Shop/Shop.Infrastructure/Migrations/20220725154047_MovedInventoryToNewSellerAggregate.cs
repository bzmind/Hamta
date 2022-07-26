using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class MovedInventoryToNewSellerAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "seller");

            migrationBuilder.RenameTable(
                name: "Inventories",
                schema: "inventory",
                newName: "Inventories",
                newSchema: "seller");

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                schema: "seller",
                table: "Inventories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Sellers",
                schema: "seller",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_SellerId",
                schema: "seller",
                table: "Inventories",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Sellers_SellerId",
                schema: "seller",
                table: "Inventories",
                column: "SellerId",
                principalSchema: "seller",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Sellers_SellerId",
                schema: "seller",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "seller");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_SellerId",
                schema: "seller",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "seller",
                table: "Inventories");

            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.RenameTable(
                name: "Inventories",
                schema: "seller",
                newName: "Inventories",
                newSchema: "inventory");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class RemovedExtraDescriptionsFromProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomSpecifications",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ExtraDescriptions",
                schema: "product");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "product",
                table: "Products",
                newName: "Introduction");

            migrationBuilder.AddColumn<string>(
                name: "Review",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specifications",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_ProductId",
                schema: "product",
                table: "Specifications",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specifications",
                schema: "product");

            migrationBuilder.DropColumn(
                name: "Review",
                schema: "product",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Introduction",
                schema: "product",
                table: "Products",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "CustomSpecifications",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomSpecifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraDescriptions",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraDescriptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomSpecifications_ProductId",
                schema: "product",
                table: "CustomSpecifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraDescriptions_ProductId",
                schema: "product",
                table: "ExtraDescriptions",
                column: "ProductId");
        }
    }
}

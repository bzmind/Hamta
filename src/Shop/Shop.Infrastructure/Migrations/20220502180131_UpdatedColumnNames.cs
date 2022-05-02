using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class UpdatedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "question");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "order");

            migrationBuilder.DropTable(
                name: "Score",
                schema: "product");

            migrationBuilder.RenameColumn(
                name: "ShippingCost_Value",
                schema: "shipping",
                table: "Shippings",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "ShippingMethod",
                schema: "shipping",
                table: "Shippings",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "ShippingInfo_ShippingMethod",
                schema: "order",
                table: "Orders",
                newName: "ShippingMethod");

            migrationBuilder.RenameColumn(
                name: "ShippingInfo_ShippingCost_Value",
                schema: "order",
                table: "Orders",
                newName: "ShippingCost");

            migrationBuilder.RenameColumn(
                name: "Price_Value",
                schema: "inventory",
                table: "Inventories",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber_Value",
                schema: "customer",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber_Value",
                schema: "order",
                table: "Addresses",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber_Value",
                schema: "customer",
                table: "Addresses",
                newName: "PhoneNumber");

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    InventoryId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                schema: "question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => new { x.QuestionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Replies_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "question",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                schema: "product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Scores_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "order");

            migrationBuilder.DropTable(
                name: "Replies",
                schema: "question");

            migrationBuilder.DropTable(
                name: "Scores",
                schema: "product");

            migrationBuilder.RenameColumn(
                name: "Cost",
                schema: "shipping",
                table: "Shippings",
                newName: "ShippingCost_Value");

            migrationBuilder.RenameColumn(
                name: "Method",
                schema: "shipping",
                table: "Shippings",
                newName: "ShippingMethod");

            migrationBuilder.RenameColumn(
                name: "ShippingMethod",
                schema: "order",
                table: "Orders",
                newName: "ShippingInfo_ShippingMethod");

            migrationBuilder.RenameColumn(
                name: "ShippingCost",
                schema: "order",
                table: "Orders",
                newName: "ShippingInfo_ShippingCost_Value");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "inventory",
                table: "Inventories",
                newName: "Price_Value");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "customer",
                table: "Customers",
                newName: "PhoneNumber_Value");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "order",
                table: "Addresses",
                newName: "PhoneNumber_Value");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "customer",
                table: "Addresses",
                newName: "PhoneNumber_Value");

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "question",
                columns: table => new
                {
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new { x.QuestionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "question",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "order",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price_Value = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InventoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                schema: "product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Score_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

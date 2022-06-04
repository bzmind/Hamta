using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangeDataTimeColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "user",
                table: "Users",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "shipping",
                table: "Shippings",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "question",
                table: "Questions",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "product",
                table: "Products",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "order",
                table: "Orders",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "inventory",
                table: "Inventories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "comment",
                table: "Comments",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "color",
                table: "Colors",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "category",
                table: "Categories",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "user",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "shipping",
                table: "Shippings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "question",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "product",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "order",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "inventory",
                table: "Inventories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "comment",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "color",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "category",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");
        }
    }
}

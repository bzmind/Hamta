﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedAvatarNameToIdInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarName",
                schema: "user",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "AvatarId",
                schema: "user",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarId",
                schema: "user",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AvatarName",
                schema: "user",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

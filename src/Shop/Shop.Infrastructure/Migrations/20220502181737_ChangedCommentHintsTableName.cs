using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class ChangedCommentHintsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentHints",
                schema: "comment");

            migrationBuilder.CreateTable(
                name: "Hints",
                schema: "comment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hints", x => new { x.CommentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Hints_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "comment",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hints",
                schema: "comment");

            migrationBuilder.CreateTable(
                name: "CommentHints",
                schema: "comment",
                columns: table => new
                {
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentHints", x => new { x.CommentId, x.Id });
                    table.ForeignKey(
                        name: "FK_CommentHints_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "comment",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

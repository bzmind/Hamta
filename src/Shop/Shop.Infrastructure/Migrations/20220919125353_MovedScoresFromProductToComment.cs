using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class MovedScoresFromProductToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores",
                schema: "product");

            migrationBuilder.AddColumn<byte>(
                name: "Score",
                schema: "comment",
                table: "Comments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                schema: "comment",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "Scores",
                schema: "product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<float>(type: "float(2)", nullable: false)
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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class CreatedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE [comment].Comments
                ADD CONSTRAINT FK_Comments_Users_UserId
                FOREIGN KEY (UserId) REFERENCES [user].Users(Id);
                ALTER TABLE [comment].Comments
                ADD CONSTRAINT FK_Comments_Products_ProductId
                FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [user].FavoriteItems
                ADD CONSTRAINT FK_FavoriteItems_Products_ProductId
                FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [inventory].Inventories
                ADD CONSTRAINT FK_Inventories_Products_ProductId
                FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [order].Items
                ADD CONSTRAINT FK_Items_Inventories_InventoryId
                FOREIGN KEY (InventoryId) REFERENCES [inventory].Inventories(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [order].Orders
                ADD CONSTRAINT FK_Orders_Users_UserId
                FOREIGN KEY (UserId) REFERENCES [user].Users(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [product].Products
                ADD CONSTRAINT FK_Products_Categories_CategoryId
                FOREIGN KEY (CategoryId) REFERENCES [category].Categories(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [question].Questions
                ADD CONSTRAINT FK_Questions_Products_ProductId
                FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);
                ALTER TABLE [question].Questions
                ADD CONSTRAINT FK_Questions_Users_UserId
                FOREIGN KEY (UserId) REFERENCES [user].Users(Id);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

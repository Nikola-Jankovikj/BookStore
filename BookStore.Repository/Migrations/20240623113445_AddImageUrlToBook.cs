using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPublisher_Books_BookId",
                table: "BookPublisher");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPublisher_Publishers_PublisherId",
                table: "BookPublisher");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_OwnerId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartBooks_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPublisher",
                table: "BookPublisher");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                newName: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "BookPublisher",
                newName: "BookPublishers");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_OwnerId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPublisher_PublisherId",
                table: "BookPublishers",
                newName: "IX_BookPublishers_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPublisher_BookId_PublisherId",
                table: "BookPublishers",
                newName: "IX_BookPublishers_BookId_PublisherId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPublishers",
                table: "BookPublishers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPublishers_Books_BookId",
                table: "BookPublishers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPublishers_Publishers_PublisherId",
                table: "BookPublishers",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartBooks_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartBooks",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_OwnerId",
                table: "ShoppingCarts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPublishers_Books_BookId",
                table: "BookPublishers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPublishers_Publishers_PublisherId",
                table: "BookPublishers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartBooks_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_OwnerId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPublishers",
                table: "BookPublishers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "BookPublishers",
                newName: "BookPublisher");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_OwnerId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPublishers_PublisherId",
                table: "BookPublisher",
                newName: "IX_BookPublisher_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPublishers_BookId_PublisherId",
                table: "BookPublisher",
                newName: "IX_BookPublisher_BookId_PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPublisher",
                table: "BookPublisher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPublisher_Books_BookId",
                table: "BookPublisher",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPublisher_Publishers_PublisherId",
                table: "BookPublisher",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_OwnerId",
                table: "ShoppingCart",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartBooks_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartBooks",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

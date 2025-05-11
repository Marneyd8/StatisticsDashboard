using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatisticsDashboard.Migrations
{
    /// <inheritdoc />
    public partial class manytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_serialNumbers_Items_ItemId",
                table: "serialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_serialNumbers",
                table: "serialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "serialNumbers",
                newName: "SerialNumbers");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_serialNumbers_ItemId",
                table: "SerialNumbers",
                newName: "IX_SerialNumbers_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerialNumbers",
                table: "SerialNumbers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemClients",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClients", x => new { x.ItemId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_ItemClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemClients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemClients_ClientId",
                table: "ItemClients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.DropTable(
                name: "ItemClients");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SerialNumbers",
                table: "SerialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "SerialNumbers",
                newName: "serialNumbers");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_SerialNumbers_ItemId",
                table: "serialNumbers",
                newName: "IX_serialNumbers_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_serialNumbers",
                table: "serialNumbers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_serialNumbers_Items_ItemId",
                table: "serialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}

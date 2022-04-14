using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Groceries.Migrations
{
    public partial class Add_Order_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerInfo_FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_Phone = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    CustomerInfo_Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerInfo_AddressRegion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_AddressCity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_AddressStreet = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_AddressHouse = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerInfo_AddressFlat = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CustomerInfo_AddressPostalCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FixedSum = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDelivered = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    OrderId = table.Column<uint>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}

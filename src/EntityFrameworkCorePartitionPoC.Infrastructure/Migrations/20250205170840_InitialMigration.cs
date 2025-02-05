using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFrameworkCorePartitionPoC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartitionKey = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartitionKey = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b7e5907-403f-468f-affe-ddf693d2ba69"), "Estee Lauder" },
                    { new Guid("dcd71abf-ad28-4cdc-bc0c-bcac0441b0f1"), "Primark" },
                    { new Guid("ec79499a-3357-40b7-a5a6-d5483692a3fc"), "Adidas" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "PartitionKey", "Volume", "Weight" },
                values: new object[,]
                {
                    { new Guid("18917576-fb65-428b-a47b-e86cf79b7355"), new Guid("dcd71abf-ad28-4cdc-bc0c-bcac0441b0f1"), new DateOnly(2024, 11, 9), "", 235.0, 288.0 },
                    { new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"), new Guid("dcd71abf-ad28-4cdc-bc0c-bcac0441b0f1"), new DateOnly(2024, 8, 1), "", 4756.0, null },
                    { new Guid("bf231903-c26c-4878-8452-52909099683d"), new Guid("dcd71abf-ad28-4cdc-bc0c-bcac0441b0f1"), new DateOnly(2025, 2, 2), "", null, 457847.0 },
                    { new Guid("cd545d42-4722-47a2-a740-923ac2532c49"), new Guid("6b7e5907-403f-468f-affe-ddf693d2ba69"), new DateOnly(2024, 5, 29), "", 12.0, 2.0 },
                    { new Guid("fca25cc3-fd41-4d57-bc3f-fab878b84e18"), new Guid("ec79499a-3357-40b7-a5a6-d5483692a3fc"), new DateOnly(2024, 2, 5), "", 42.0, 2342.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Item", "OrderId", "PartitionKey", "Price" },
                values: new object[,]
                {
                    { new Guid("3d310b25-d3c2-48cd-b8cc-f926eecc9060"), "Item 1", new Guid("cd545d42-4722-47a2-a740-923ac2532c49"), "", 238.12m },
                    { new Guid("3e756ab8-3b62-41a6-8a75-b6f8347f4f01"), "Item 9", new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"), "", 988.00m },
                    { new Guid("454b3e7a-65ec-44a2-bc2f-2afc9fce2989"), "Item 4", new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"), "", 88.00m },
                    { new Guid("521e6807-ed49-4e3e-9323-a3fc16e653ab"), "Item 4", new Guid("cd545d42-4722-47a2-a740-923ac2532c49"), "", 332.00m },
                    { new Guid("5546d99e-346b-4333-a716-7a30a562feac"), "Item 9", new Guid("18917576-fb65-428b-a47b-e86cf79b7355"), "", 4576.00m },
                    { new Guid("704cf48c-8a74-49ee-a283-0f8af2aa880a"), "Item 8", new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"), "", 3.00m },
                    { new Guid("718d2717-e71a-42c8-9638-59c78c3a6f95"), "Item 1", new Guid("fca25cc3-fd41-4d57-bc3f-fab878b84e18"), "", 238.12m },
                    { new Guid("7fa197cb-950f-42e4-89bd-36ee23b8358a"), "Item 8", new Guid("bf231903-c26c-4878-8452-52909099683d"), "", 123.87m },
                    { new Guid("90879c03-1e43-486f-8f02-50ba4b9cc4a0"), "Item 7", new Guid("18917576-fb65-428b-a47b-e86cf79b7355"), "", 223.00m },
                    { new Guid("b5f7c577-89b4-41dd-814a-b4d290deb733"), "Item 5", new Guid("bf231903-c26c-4878-8452-52909099683d"), "", 89.25m },
                    { new Guid("ba0a7a3f-0a6a-41fb-92df-7f77e3baaa65"), "Item 2", new Guid("fca25cc3-fd41-4d57-bc3f-fab878b84e18"), "", 5776.43m },
                    { new Guid("d4d78920-0faa-438d-9bc3-73e9e5a5cbb2"), "Item 7", new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"), "", 223.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

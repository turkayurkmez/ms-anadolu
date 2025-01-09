using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2356), null, "Elektronik" },
                    { 2, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2359), null, "Kırtasiye" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "LastModifiedDate", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("2bb7aed0-3021-4478-b793-f0484f5717ce"), 2, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2402), "A5 Defter", "defter.jpg", null, "Defter", 20m, 100 },
                    { new Guid("3f7fc6a0-1746-4ddb-aa35-94f44a5eb945"), 2, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2400), "Kırmızı Kalem", "kalem.jpg", null, "Kalem", 10m, 100 },
                    { new Guid("3f95ca6a-fa07-4251-92d9-0cdb70fd9172"), 1, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2398), "Samsung S21", "samsungs21.jpg", null, "Samsung S21", 9000m, 100 },
                    { new Guid("4bcc145a-0098-4c76-abab-c27c285935cd"), 1, new DateTime(2025, 1, 9, 8, 31, 32, 875, DateTimeKind.Utc).AddTicks(2393), "Apple Iphone 12", "iphone12.jpg", null, "Iphone 12", 10000m, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodStore.Infrastrucutre.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InStore = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "Seafood" },
                    { new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "Bakery" },
                    { new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "Snacks" },
                    { new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), "Beverages" },
                    { new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "Deli" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "InStore", "Price", "ProductDescription", "ProductName" },
                values: new object[,]
                {
                    { new Guid("0b81647d-db2d-4d38-bac8-3cdbe38b8210"), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), true, 5.00m, "Flaminco smoked cheese puffs 40g", "Falminco" },
                    { new Guid("1734ea16-2ae6-4c6a-a46e-641c6b9a8a62"), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), true, 10.00m, "Chocolate Molto from edita", "Molto" },
                    { new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), true, 160.00m, "Rich smoked turkey breast", "Turkey" },
                    { new Guid("29339209-63f5-492f-8459-754943c74abf"), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), true, 90.00m, "Sardines from red sea", "Sardines" },
                    { new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), true, 250.00m, "Shrimps from red sea", "Shrimps" },
                    { new Guid("3eae53b3-1472-4465-b361-6703924fa6c6"), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), true, 5.00m, "Abu Auf popcorn salted and caramel 100g", "Popcorn" },
                    { new Guid("7be9243c-19ac-4b4c-8319-9211f99d4216"), new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), true, 10.25m, "Bake Rolz ketchup family size 90g", "Bake Rolz" },
                    { new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"), new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), true, 3.00m, "White bread with made from whole grains, seeds, and fiber-rich flour", "Diet bread" },
                    { new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), true, 130.00m, "Halwani Pastirma", "Pastirma" },
                    { new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"), new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), true, 60.00m, "Nile tilapia from nile river", "Nile tilapia" },
                    { new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"), new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), true, 20.00m, "Sparkling water with lemon flavor", "Green Cola" },
                    { new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"), new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), true, 15.00m, "Sparkling water with orange flavor", "SpiroSpatis" },
                    { new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"), new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), true, 5.00m, "Mineral water with low sodium", "Mineral water" },
                    { new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), true, 120.00m, "Low salt Feta cheese from Domty", "Feta cheese" },
                    { new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"), new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), true, 2.00m, "White bread with made from wheat flour", "White bread" },
                    { new Guid("df660a73-c0b7-4340-abc1-a914257a5674"), new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), true, 350.00m, "Cheddar cheese with olives from Holland", "Cheddar cheese" }
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore.Infrastrucutre.Migrations
{
    /// <inheritdoc />
    public partial class SetNullDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                column: "ProductId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                column: "ProductId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                column: "ProductId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                column: "ProductId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                column: "ProductId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductId",
                table: "Categories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Categories");
        }
    }
}

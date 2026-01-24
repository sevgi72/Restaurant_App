using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId1",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId1",
                table: "OrderItems",
                column: "MenuItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemId1",
                table: "OrderItems",
                column: "MenuItemId1",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemId1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_MenuItemId1",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuItemId1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "MenuItems");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Pizza", "Margherita Pizza", 102.00m },
                    { 2, "Burger", "Cheeseburger", 95.50m },
                    { 3, "Drink", "Coca Cola", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150.50m },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 320.00m },
                    { 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Count", "MenuItemId", "OrderId" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 1, 3, 1 },
                    { 3, 3, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}

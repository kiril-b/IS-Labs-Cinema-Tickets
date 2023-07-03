using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_ShoppingCarts_ShoppingCartId",
                table: "TicketInOrders");

            migrationBuilder.DropIndex(
                name: "IX_TicketInOrders_ShoppingCartId",
                table: "TicketInOrders");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "TicketInOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "TicketInOrders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "TicketInOrders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "TicketInOrders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketInOrders_ShoppingCartId",
                table: "TicketInOrders",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_ShoppingCarts_ShoppingCartId",
                table: "TicketInOrders",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

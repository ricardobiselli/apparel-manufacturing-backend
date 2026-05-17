using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MachineSessionHasOrderIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "MachineSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_OrderId",
                table: "MachineSessions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSessions_Orders_OrderId",
                table: "MachineSessions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineSessions_Orders_OrderId",
                table: "MachineSessions");

            migrationBuilder.DropIndex(
                name: "IX_MachineSessions_OrderId",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MachineSessions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MachineSessionNowHasUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MachineSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_UserId",
                table: "MachineSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSessions_Users_UserId",
                table: "MachineSessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineSessions_Users_UserId",
                table: "MachineSessions");

            migrationBuilder.DropIndex(
                name: "IX_MachineSessions_UserId",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MachineSessions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveMachineSessionIdToMachineEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineExceptionLog_MachineSes~",
                table: "MachineEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineSessionId",
                table: "MachineEvent");

            migrationBuilder.DropIndex(
                name: "IX_MachineEvent_MachineExceptionLog_MachineSessionId",
                table: "MachineEvent");

            migrationBuilder.DropColumn(
                name: "MachineExceptionLog_MachineSessionId",
                table: "MachineEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineSessionId",
                table: "MachineEvent",
                column: "MachineSessionId",
                principalTable: "MachineSessions",
                principalColumn: "MachineSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineSessionId",
                table: "MachineEvent");

            migrationBuilder.AddColumn<int>(
                name: "MachineExceptionLog_MachineSessionId",
                table: "MachineEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvent_MachineExceptionLog_MachineSessionId",
                table: "MachineEvent",
                column: "MachineExceptionLog_MachineSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineExceptionLog_MachineSes~",
                table: "MachineEvent",
                column: "MachineExceptionLog_MachineSessionId",
                principalTable: "MachineSessions",
                principalColumn: "MachineSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvent_MachineSessions_MachineSessionId",
                table: "MachineEvent",
                column: "MachineSessionId",
                principalTable: "MachineSessions",
                principalColumn: "MachineSessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MachineSession1to1RelationshipWithOperationReplacedBySnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineSessions_Operations_OperationId",
                table: "MachineSessions");

            migrationBuilder.DropIndex(
                name: "IX_MachineSessions_OperationId",
                table: "MachineSessions");

            migrationBuilder.AddColumn<double>(
                name: "BaseTime",
                table: "MachineSessions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "OperationDescription",
                table: "MachineSessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OperationName",
                table: "MachineSessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UnitsPerGarment",
                table: "MachineSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseTime",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "OperationDescription",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "OperationName",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "UnitsPerGarment",
                table: "MachineSessions");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_OperationId",
                table: "MachineSessions",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSessions_Operations_OperationId",
                table: "MachineSessions",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "OperationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

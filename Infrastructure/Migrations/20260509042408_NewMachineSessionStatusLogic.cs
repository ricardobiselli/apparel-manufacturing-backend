using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMachineSessionStatusLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MachineSessions");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MachineSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MachineSessions");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MachineSessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

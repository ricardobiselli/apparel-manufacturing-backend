using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garments",
                columns: table => new
                {
                    GarmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GarmentName = table.Column<string>(type: "text", nullable: false),
                    GarmentDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garments", x => x.GarmentId);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostNumber = table.Column<int>(type: "integer", nullable: false),
                    MachineName = table.Column<string>(type: "text", nullable: false),
                    MachineModel = table.Column<string>(type: "text", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InstallDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeIdNumber = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationName = table.Column<string>(type: "text", nullable: false),
                    OperationDescription = table.Column<string>(type: "text", nullable: false),
                    GarmentId = table.Column<int>(type: "integer", nullable: false),
                    BaseTime = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_Operations_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "GarmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGarments",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    GarmentId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGarments", x => new { x.OrderId, x.GarmentId });
                    table.ForeignKey(
                        name: "FK_OrderGarments_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "GarmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGarments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineSessions",
                columns: table => new
                {
                    MachineSessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineId = table.Column<int>(type: "integer", nullable: false),
                    GarmentId = table.Column<int>(type: "integer", nullable: false),
                    OperationId = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSessions", x => x.MachineSessionId);
                    table.ForeignKey(
                        name: "FK_MachineSessions_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "GarmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineSessions_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineSessions_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineEvent",
                columns: table => new
                {
                    MachineEventId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineId = table.Column<int>(type: "integer", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    MachineSessionId = table.Column<int>(type: "integer", nullable: true),
                    MachineExceptionLog_MachineSessionId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    TimeSpentSeconds = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineEvent", x => x.MachineEventId);
                    table.ForeignKey(
                        name: "FK_MachineEvent_MachineSessions_MachineExceptionLog_MachineSes~",
                        column: x => x.MachineExceptionLog_MachineSessionId,
                        principalTable: "MachineSessions",
                        principalColumn: "MachineSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineEvent_MachineSessions_MachineSessionId",
                        column: x => x.MachineSessionId,
                        principalTable: "MachineSessions",
                        principalColumn: "MachineSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineEvent_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvent_MachineExceptionLog_MachineSessionId",
                table: "MachineEvent",
                column: "MachineExceptionLog_MachineSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvent_MachineId",
                table: "MachineEvent",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvent_MachineSessionId",
                table: "MachineEvent",
                column: "MachineSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_GarmentId",
                table: "MachineSessions",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_MachineId",
                table: "MachineSessions",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_OperationId",
                table: "MachineSessions",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_GarmentId",
                table: "Operations",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGarments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineEvent");

            migrationBuilder.DropTable(
                name: "OrderGarments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MachineSessions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Garments");
        }
    }
}

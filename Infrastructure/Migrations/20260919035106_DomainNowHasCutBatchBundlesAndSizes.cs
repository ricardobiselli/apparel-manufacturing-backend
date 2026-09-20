using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DomainNowHasCutBatchBundlesAndSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BundleId",
                table: "MachineSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CutBatches",
                columns: table => new
                {
                    CutBatchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    GarmentId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CutBatches", x => x.CutBatchId);
                    table.ForeignKey(
                        name: "FK_CutBatches_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "GarmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CutBatches_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    BundleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CutBatchId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.BundleId);
                    table.ForeignKey(
                        name: "FK_Bundles_CutBatches_CutBatchId",
                        column: x => x.CutBatchId,
                        principalTable: "CutBatches",
                        principalColumn: "CutBatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CutBatchSizes",
                columns: table => new
                {
                    CutBatchId = table.Column<int>(type: "integer", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CutBatchSizes", x => new { x.CutBatchId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_CutBatchSizes_CutBatches_CutBatchId",
                        column: x => x.CutBatchId,
                        principalTable: "CutBatches",
                        principalColumn: "CutBatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CutBatchSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BundleSizes",
                columns: table => new
                {
                    BundleId = table.Column<int>(type: "integer", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleSizes", x => new { x.BundleId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_BundleSizes_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BundleSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_BundleId",
                table: "MachineSessions",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bundles_CutBatchId",
                table: "Bundles",
                column: "CutBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleSizes_SizeId",
                table: "BundleSizes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_CutBatches_GarmentId",
                table: "CutBatches",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CutBatches_OrderId",
                table: "CutBatches",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CutBatchSizes_SizeId",
                table: "CutBatchSizes",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSessions_Bundles_BundleId",
                table: "MachineSessions",
                column: "BundleId",
                principalTable: "Bundles",
                principalColumn: "BundleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineSessions_Bundles_BundleId",
                table: "MachineSessions");

            migrationBuilder.DropTable(
                name: "BundleSizes");

            migrationBuilder.DropTable(
                name: "CutBatchSizes");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "CutBatches");

            migrationBuilder.DropIndex(
                name: "IX_MachineSessions_BundleId",
                table: "MachineSessions");

            migrationBuilder.DropColumn(
                name: "BundleId",
                table: "MachineSessions");
        }
    }
}

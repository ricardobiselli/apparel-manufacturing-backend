using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlannedVsActualQuantityConceptIntroduced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CutBatchSizes",
                newName: "PlannedQuantity");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CutBatches",
                newName: "PlannedQuantity");

            migrationBuilder.AddColumn<int>(
                name: "ActualQuantity",
                table: "CutBatchSizes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualQuantity",
                table: "CutBatches",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderGarmentSizes",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    GarmentId = table.Column<int>(type: "integer", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGarmentSizes", x => new { x.OrderId, x.GarmentId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_OrderGarmentSizes_OrderGarments_OrderId_GarmentId",
                        columns: x => new { x.OrderId, x.GarmentId },
                        principalTable: "OrderGarments",
                        principalColumns: new[] { "OrderId", "GarmentId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGarmentSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderGarmentSizes_SizeId",
                table: "OrderGarmentSizes",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderGarmentSizes");

            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "CutBatchSizes");

            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "CutBatches");

            migrationBuilder.RenameColumn(
                name: "PlannedQuantity",
                table: "CutBatchSizes",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PlannedQuantity",
                table: "CutBatches",
                newName: "Quantity");
        }
    }
}

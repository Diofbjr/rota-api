using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rota.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "RouteRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxWeightKg = table.Column<double>(type: "float", nullable: false),
                    MaxVolumeM3 = table.Column<double>(type: "float", nullable: false),
                    CostPerKm = table.Column<double>(type: "float", nullable: false),
                    CostPerHour = table.Column<double>(type: "float", nullable: false),
                    DriverCostPerHour = table.Column<double>(type: "float", nullable: false),
                    MaxDistanceWithoutRefuelKm = table.Column<double>(type: "float", nullable: false),
                    MaxHeightM = table.Column<double>(type: "float", nullable: false),
                    MaxLoadWeightKg = table.Column<double>(type: "float", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteRequests_VehicleId",
                table: "RouteRequests",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteRequests_Vehicle_VehicleId",
                table: "RouteRequests",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteRequests_Vehicle_VehicleId",
                table: "RouteRequests");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_RouteRequests_VehicleId",
                table: "RouteRequests");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "RouteRequests");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rota.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optimization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteRequestId = table.Column<int>(type: "int", nullable: false),
                    TotalDistanceKm = table.Column<double>(type: "float", nullable: false),
                    TotalTimeMinutes = table.Column<double>(type: "float", nullable: false),
                    SerializedPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteResults_RouteRequests_RouteRequestId",
                        column: x => x.RouteRequestId,
                        principalTable: "RouteRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waypoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    RouteRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waypoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waypoints_RouteRequests_RouteRequestId",
                        column: x => x.RouteRequestId,
                        principalTable: "RouteRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteResults_RouteRequestId",
                table: "RouteResults",
                column: "RouteRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Waypoints_RouteRequestId",
                table: "Waypoints",
                column: "RouteRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteResults");

            migrationBuilder.DropTable(
                name: "Waypoints");

            migrationBuilder.DropTable(
                name: "RouteRequests");
        }
    }
}

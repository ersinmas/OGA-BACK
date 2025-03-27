using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    TrailerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNumber = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxWeight = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.TrailerId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.VehicleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    RegNumber = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "VehicleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTrailers",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TrailerId = table.Column<int>(type: "int", nullable: false),
                    BegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTrailers", x => new { x.VehicleId, x.TrailerId, x.BegDate });
                    table.ForeignKey(
                        name: "FK_VehicleTrailers_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "TrailerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleTrailers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTrailers_TrailerId",
                table: "VehicleTrailers",
                column: "TrailerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTrailers");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}

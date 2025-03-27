using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTrailers",
                table: "VehicleTrailers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTrailers",
                table: "VehicleTrailers",
                columns: new[] { "VehicleId", "TrailerId", "BegDate", "EndDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTrailers",
                table: "VehicleTrailers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTrailers",
                table: "VehicleTrailers",
                columns: new[] { "VehicleId", "TrailerId", "BegDate" });
        }
    }
}

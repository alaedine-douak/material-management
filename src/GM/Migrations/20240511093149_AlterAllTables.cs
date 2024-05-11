using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GM.Migrations
{
    /// <inheritdoc />
    public partial class AlterAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertedDuration",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "DocumentInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "DocumentInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfos_VehicleId",
                table: "DocumentInfos",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentInfos_Vehicles_VehicleId",
                table: "DocumentInfos",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentInfos_Vehicles_VehicleId",
                table: "DocumentInfos");

            migrationBuilder.DropIndex(
                name: "IX_DocumentInfos_VehicleId",
                table: "DocumentInfos");

            migrationBuilder.DropColumn(
                name: "AlertedDuration",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "DocumentInfos");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "DocumentInfos");
        }
    }
}

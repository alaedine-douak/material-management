using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GM.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Code_PlateNumber",
                table: "Vehicles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Code_PlateNumber",
                table: "Vehicles",
                columns: new[] { "Code", "PlateNumber" },
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GM.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyNameToAlertDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlertedDuration",
                table: "Documents",
                newName: "AlertDuration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlertDuration",
                table: "Documents",
                newName: "AlertedDuration");
        }
    }
}

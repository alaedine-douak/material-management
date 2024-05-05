using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GM.Migrations
{
    /// <inheritdoc />
    public partial class AlterDocumentInfoDatesTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedDate",
                table: "DocumentInfos",
                type: "timestamp(0) without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "DocumentInfos",
                type: "timestamp(0) without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedDate",
                table: "DocumentInfos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(0) without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "DocumentInfos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(0) without time zone");
        }
    }
}

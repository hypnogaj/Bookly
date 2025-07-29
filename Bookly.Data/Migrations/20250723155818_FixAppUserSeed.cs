using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAppUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 58, 17, 479, DateTimeKind.Local).AddTicks(5584));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 58, 17, 479, DateTimeKind.Local).AddTicks(6872));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 58, 17, 479, DateTimeKind.Local).AddTicks(6876));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 58, 17, 479, DateTimeKind.Local).AddTicks(6877));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 7, 23, 18, 45, 39, 932, DateTimeKind.Local).AddTicks(3746), new Guid("cabcedd4-0f13-41fa-b90e-389b9ee33ee9") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 45, 39, 937, DateTimeKind.Local).AddTicks(2184));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 45, 39, 937, DateTimeKind.Local).AddTicks(3494));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 45, 39, 937, DateTimeKind.Local).AddTicks(3498));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 18, 45, 39, 937, DateTimeKind.Local).AddTicks(3499));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdreslerEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 15, 32, 807, DateTimeKind.Local).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 15, 32, 807, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 15, 32, 807, DateTimeKind.Local).AddTicks(5401));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 15, 32, 807, DateTimeKind.Local).AddTicks(5402));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 14, 44, 620, DateTimeKind.Local).AddTicks(3507));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 14, 44, 620, DateTimeKind.Local).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 14, 44, 620, DateTimeKind.Local).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 14, 44, 620, DateTimeKind.Local).AddTicks(5762));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 17, 9, 186, DateTimeKind.Local).AddTicks(3668));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 17, 9, 186, DateTimeKind.Local).AddTicks(6915));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 17, 9, 186, DateTimeKind.Local).AddTicks(6924));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 17, 9, 186, DateTimeKind.Local).AddTicks(6926));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 16, 22, 613, DateTimeKind.Local).AddTicks(374));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 16, 22, 613, DateTimeKind.Local).AddTicks(2917));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 16, 22, 613, DateTimeKind.Local).AddTicks(2925));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2025, 7, 23, 19, 16, 22, 613, DateTimeKind.Local).AddTicks(2927));
        }
    }
}

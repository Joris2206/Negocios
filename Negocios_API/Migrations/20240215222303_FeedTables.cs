using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegociosAPI.Migrations
{
    /// <inheritdoc />
    public partial class FeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(695), new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(694) });

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(697), new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(697) });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(615), new DateTime(2024, 2, 15, 16, 23, 3, 713, DateTimeKind.Local).AddTicks(606) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3611), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3613), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3525), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3516) });
        }
    }
}

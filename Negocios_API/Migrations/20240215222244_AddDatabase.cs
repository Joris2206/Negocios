using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NegociosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePropietario = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    NombreNegocio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUC = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Clave", "Correo", "FechaActualizacion", "FechaCreacion", "NombrePropietario" },
                values: new object[] { 1, "abcd1234", "josephpineda1210@gmail.com", new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3525), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3516), "Joseph Enmanuel Pineda Aguilera" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Direccion", "FechaActualizacion", "FechaCreacion", "NombreNegocio", "OwnerId", "RUC" },
                values: new object[,]
                {
                    { 1, "Linda Vista", new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3611), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3610), "Capri", 1, 123456789 },
                    { 2, "Bolonia", new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3613), new DateTime(2024, 2, 15, 16, 22, 44, 541, DateTimeKind.Local).AddTicks(3612), "Serta", 1, 11223344 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}

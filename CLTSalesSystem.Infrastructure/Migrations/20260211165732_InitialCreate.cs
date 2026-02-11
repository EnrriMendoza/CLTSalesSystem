using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLTSalesSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefono = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Precio = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VENTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Total = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENTAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DETALLES_VENTA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    VentaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cantidad = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLES_VENTA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DETALLES_VENTA_VENTAS_VentaId",
                        column: x => x.VentaId,
                        principalTable: "VENTAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DETALLES_VENTA_VentaId",
                table: "DETALLES_VENTA",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES");

            migrationBuilder.DropTable(
                name: "DETALLES_VENTA");

            migrationBuilder.DropTable(
                name: "PRODUCTOS");

            migrationBuilder.DropTable(
                name: "VENTAS");
        }
    }
}

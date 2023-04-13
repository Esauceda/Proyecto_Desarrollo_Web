using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Desarrollo_Web.Migrations
{
    public partial class comprasdetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompraDetalle",
                columns: table => new
                {
                    CompraDetalleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompraEncabezadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Cantidad = table.Column<string>(type: "varchar(6)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDetalle", x => x.CompraDetalleId);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_CompraEncabezado_CompraEncabezadoId",
                        column: x => x.CompraEncabezadoId,
                        principalTable: "CompraEncabezado",
                        principalColumn: "CompraEncabezadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_CompraEncabezadoId",
                table: "CompraDetalle",
                column: "CompraEncabezadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_ProductoId",
                table: "CompraDetalle",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDetalle");
        }
    }
}

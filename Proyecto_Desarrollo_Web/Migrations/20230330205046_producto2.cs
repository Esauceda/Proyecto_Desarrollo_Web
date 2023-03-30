using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Desarrollo_Web.Migrations
{
    public partial class producto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaId",
                table: "Producto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Producto",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(40)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "CompraEncabezado",
                columns: table => new
                {
                    CompraEncabezadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroFactura = table.Column<string>(type: "varchar(50)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraEncabezado", x => x.CompraEncabezadoId);
                    table.ForeignKey(
                        name: "FK_CompraEncabezado_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaId",
                table: "Producto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraEncabezado_ProveedorId",
                table: "CompraEncabezado",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoria_CategoriaId",
                table: "Producto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoria_CategoriaId",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "CompraEncabezado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_CategoriaId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Producto");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Desarrollo_Web.Migrations
{
    public partial class Cliente_Orden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "Producto",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "CompraDetalle",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Apellido = table.Column<string>(type: "varchar(100)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(8)", nullable: true),
                    Correo = table.Column<string>(type: "varchar(100)", nullable: true),
                    DNI = table.Column<string>(type: "varchar(100)", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "OrdenEncabezado",
                columns: table => new
                {
                    OrdenEncabezadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenEncabezado", x => x.OrdenEncabezadoId);
                    table.ForeignKey(
                        name: "FK_OrdenEncabezado_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenDetalle",
                columns: table => new
                {
                    OrdenDetalleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdenEncabezadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Cantidad = table.Column<string>(type: "varchar(6)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDetalle", x => x.OrdenDetalleId);
                    table.ForeignKey(
                        name: "FK_OrdenDetalle_OrdenEncabezado_OrdenEncabezadoId",
                        column: x => x.OrdenEncabezadoId,
                        principalTable: "OrdenEncabezado",
                        principalColumn: "OrdenEncabezadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenDetalle_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_OrdenEncabezadoId",
                table: "OrdenDetalle",
                column: "OrdenEncabezadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDetalle_ProductoId",
                table: "OrdenDetalle",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEncabezado_ClienteId",
                table: "OrdenEncabezado",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenDetalle");

            migrationBuilder.DropTable(
                name: "OrdenEncabezado");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "Producto",
                type: "varchar(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Cantidad",
                table: "CompraDetalle",
                type: "varchar(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)");
        }
    }
}

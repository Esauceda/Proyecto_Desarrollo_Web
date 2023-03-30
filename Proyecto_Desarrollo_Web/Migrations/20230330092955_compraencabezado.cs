using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Desarrollo_Web.Migrations
{
    public partial class compraencabezado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "CompraEncabezado",
                newName: "Eliminado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Eliminado",
                table: "CompraEncabezado",
                newName: "Estado");
        }
    }
}

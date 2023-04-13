using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class OrdenDetalle
    {
        public Guid OrdenDetalleId { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public OrdenEncabezado OrdenEncabezado { get; set; }
        public Guid OrdenEncabezadoId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public bool Eliminado { get; set; }
    }
}

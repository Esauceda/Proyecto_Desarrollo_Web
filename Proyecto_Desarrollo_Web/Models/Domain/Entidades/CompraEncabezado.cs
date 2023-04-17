using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class CompraEncabezado
    {
        public Guid CompraEncabezadoId { get; set; }
        public Proveedor Proveedor { get; set; }
        public Guid ProveedorId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string NumeroFactura { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<CompraDetalle> CompraDetalles { get; set; }
        public CompraEncabezado()
        {
            CompraDetalles = new HashSet<CompraDetalle>();
        }

        public static CompraEncabezado Create(string NumeroFactura, Guid ProveedorId, DateTime FechaSolicitud, DateTime FechaEntrega)
        {
            return new CompraEncabezado
            {
                NumeroFactura = NumeroFactura,
                ProveedorId = ProveedorId,
                FechaSolicitud = FechaSolicitud,
                FechaEntrega = FechaEntrega,
                CompraEncabezadoId = Guid.NewGuid()
            };
        }
        public void Update(string NumeroFactura, DateTime FechaSolicitud, DateTime FechaEntrega)
        {
            this.NumeroFactura = NumeroFactura;
            this.FechaSolicitud = FechaSolicitud;
            this.FechaEntrega = FechaEntrega;
 

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

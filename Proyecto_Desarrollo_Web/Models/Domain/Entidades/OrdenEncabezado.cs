using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class OrdenEncabezado
    {
        public Guid OrdenEncabezadoId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<OrdenDetalle> OrdenDetalles { get; set; }
        public OrdenEncabezado()
        {
            OrdenDetalles = new HashSet<OrdenDetalle>();
        }

        public static OrdenEncabezado Create(DateTime Fecha, Guid ClienteId)
        {
            return new OrdenEncabezado
            {
                Fecha = Fecha,
                ClienteId = ClienteId,
                OrdenEncabezadoId = Guid.NewGuid()
            };
        }
        public void Update(DateTime Fecha, Guid ClienteId)
        {
            this.Fecha = Fecha;
            this.ClienteId = ClienteId;

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Proveedor
    {
        public Guid ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public ICollection<CompraEncabezado> CompraEncabezados { get; set; }
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
            CompraEncabezados = new HashSet<CompraEncabezado>();
        }

        public static Proveedor Create(string Nombre, string Telefono)
        {
            return new Proveedor
            {
                Nombre = Nombre,
                Telefono = Telefono,
                ProveedorId = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }
        public void Update(string Nombre, string Telefono)
        {
            this.Nombre = Nombre;
            this.Telefono = Telefono;

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

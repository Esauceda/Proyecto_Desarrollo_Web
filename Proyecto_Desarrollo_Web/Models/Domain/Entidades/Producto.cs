using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Producto
    {
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public decimal Precio { get; set; }
        public Proveedor Proveedor { get; set; }
        public Guid ProveedorId { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public static Producto Create(string Nombre, string Descripcion, string Cantidad, decimal Precio, Guid ProveedorId)
        {
            return new Producto
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Cantidad    = Cantidad,
                Precio      = Precio,
                ProveedorId = ProveedorId,
                ProductoId = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }
        public void Update(string Nombre, string Descripcion, string Cantidad, decimal Precio, Guid ProveedorId)
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.ProveedorId = ProveedorId;

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Producto
    {
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public Proveedor Proveedor { get; set; }
        public Guid ProveedorId { get; set; }
        public Categoria Categoria { get; set; }
        public Guid CategoriaId { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CompraDetalle> CompraDetalles { get; set; }
        public ICollection<OrdenDetalle> OrdenDetalles { get; set; }
        public Producto()
        {
            CompraDetalles = new HashSet<CompraDetalle>();
            OrdenDetalles = new HashSet<OrdenDetalle>();
        }

        public static Producto Create(string Nombre, string Descripcion, int Cantidad, decimal Precio, Guid ProveedorId, Guid CategoriaId)
        {
            return new Producto
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Cantidad    = Cantidad,
                Precio      = Precio,
                ProveedorId = ProveedorId,
                CategoriaId = CategoriaId,
                ProductoId = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }
        public void Update(string Nombre, string Descripcion, int Cantidad, decimal Precio, Guid ProveedorId, Guid CategoriaId)
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.ProveedorId = ProveedorId;
            this.CategoriaId = CategoriaId;
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
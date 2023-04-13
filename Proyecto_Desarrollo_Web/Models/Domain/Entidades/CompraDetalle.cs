using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class CompraDetalle
    {
        public Guid CompraDetalleId { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public CompraEncabezado CompraEncabezado { get; set; }
        public Guid CompraEncabezadoId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public bool Eliminado { get; set; }

        public static CompraDetalle Create(Guid ProductoId, Guid CompraEncabezadoId, decimal Precio, int Cantidad)
        {
            return new CompraDetalle
            {
                ProductoId = ProductoId,
                CompraEncabezadoId = CompraEncabezadoId,
                Precio = Precio,
                Cantidad = Cantidad,
                CompraDetalleId = Guid.NewGuid()
            };
        }
        public void Update(Guid ProductoId, Guid CompraEncabezadoId, decimal Precio, int Cantidad)
        {
            this.ProductoId = ProductoId;
            this.CompraEncabezadoId = CompraEncabezadoId;
            this.Precio = Precio;
            this.Cantidad = Cantidad;

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}


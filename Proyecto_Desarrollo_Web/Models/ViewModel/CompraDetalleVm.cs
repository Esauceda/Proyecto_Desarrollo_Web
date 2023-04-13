using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class CompraDetalleVM
    {
        public Guid CompraDetalleId { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public CompraEncabezado CompraEncabezado { get; set; }
        public Guid CompraEncabezadoId { get; set; }
        public List<SelectListItem> CompraEncabezados { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (this.Cantidad == null)
            {
                app.Mensaje += "La cantidad no puede estar vacioa";
            }
            if (this.ProductoId == null || this.ProductoId == Guid.Empty)
            {
                app.Mensaje += "El producto id no puede estar vacio";
            }
            if (this.CompraEncabezadoId == null || this.CompraEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La compra encabezado id no puede estar vacio";
            }
            if (this.Precio == null)
            {
                app.Mensaje += "El precio no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Compra encabezado ingresado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
        public AppResult ValidarUpdate()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (this.CompraDetalleId == null || this.CompraDetalleId == Guid.Empty)
            {
                app.Mensaje += "La compra detalle id no puede estar vacia";
            }
            if (this.Cantidad == null)
            {
                app.Mensaje += "La cantidad no puede estar vacioa";
            }
            if (this.ProductoId == null || this.ProductoId == Guid.Empty)
            {
                app.Mensaje += "El producto id no puede estar vacio";
            }
            if (this.CompraEncabezadoId == null || this.CompraEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La compra encabezado id no puede estar vacio";
            }
            if (this.Precio == null)
            {
                app.Mensaje += "El precio no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Compra encabezado modificado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
        public AppResult ValidarDelete()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (this.CompraEncabezadoId == null || this.CompraEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "El campo de compra encabezado id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Compra encabezado eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

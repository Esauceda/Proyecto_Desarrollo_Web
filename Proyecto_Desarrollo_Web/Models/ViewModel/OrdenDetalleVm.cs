using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class OrdenDetalleVm
    {
        public Guid OrdenDetalleId { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public OrdenEncabezado OrdenEncabezado { get; set; }
        public Guid OrdenEncabezadoId { get; set; }
        public List<SelectListItem> OrdenEncabezados { get; set; }
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
            if (this.OrdenEncabezadoId == null || this.OrdenEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La orden encabezado id no puede estar vacio";
            }
            if (this.Precio == null)
            {
                app.Mensaje += "El precio no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Orden ingresada correctamente";
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
            if (this.OrdenDetalleId == null || this.OrdenDetalleId == Guid.Empty)
            {
                app.Mensaje += "La orden detalle id no puede estar vacia";
            }
            if (this.Cantidad == null)
            {
                app.Mensaje += "La cantidad no puede estar vacioa";
            }
            if (this.ProductoId == null || this.ProductoId == Guid.Empty)
            {
                app.Mensaje += "El producto id no puede estar vacio";
            }
            if (this.OrdenEncabezadoId == null || this.OrdenEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La orden encabezado id no puede estar vacio";
            }
            if (this.Precio == null)
            {
                app.Mensaje += "El precio no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Orden modificado correctamente";
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

            if (this.OrdenEncabezadoId == null || this.OrdenEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "El campo de orden encabezado id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Orden eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

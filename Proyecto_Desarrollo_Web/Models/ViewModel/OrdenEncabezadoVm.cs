using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class OrdenEncabezadoVm
    {
        public Guid OrdenEncabezadoId { get; set; }
        public Guid ClienteId { get; set; }
        public ClienteVm Cliente { get; set; }
        public List<SelectListItem> Clientes { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;

        //Orden Detalle
        public Guid OrdenDetalleId { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public OrdenEncabezado OrdenEncabezado { get; set; }
        public List<SelectListItem> OrdenEncabezados { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (this.ClienteId == null || this.ClienteId == Guid.Empty)
            {
                app.Mensaje += "Cliente id no puede estar vacio";
            }
            if (this.Fecha == null)
            {
                app.Mensaje += "El campo de fecha no puede estar vacio";
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
            if (this.OrdenEncabezadoId == null || this.OrdenEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La Orden encabezado id no puede estar vacia";
            }
            if (this.ClienteId == null || this.ClienteId == Guid.Empty)
            {
                app.Mensaje += "Cliente id no puede estar vacio";
            }
            if (this.Fecha == null)
            {
                app.Mensaje += "El campo de fecha no puede estar vacio";
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
                app.Mensaje = "Orden eliminada correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

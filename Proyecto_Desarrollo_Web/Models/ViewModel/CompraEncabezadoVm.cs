using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class CompraEncabezadoVm
    {
        public Guid CompraEncabezadoId { get; set; }
        public Guid ProveedorId { get; set; }
        public ProveedorVm Proveedor { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Today;
        public DateTime FechaEntrega { get; set; } = DateTime.Today;
        public string NumeroFactura { get; set; }

        //Datos CompraDetalle

        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public CompraEncabezado CompraEncabezado { get; set; }
        public List<SelectListItem> CompraEncabezados { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (string.IsNullOrEmpty(this.NumeroFactura))
            {
                app.Mensaje += "El campo de numero de factura no puede estar vacio";
            }
            if (this.ProveedorId == null || this.ProveedorId == Guid.Empty)
            {
                app.Mensaje += "El proveedor id no puede estar vacio";
            }
            if (this.FechaSolicitud == null)
            {
                app.Mensaje += "El campo de fecha de solicitud no puede estar vacio";
            }
            if (this.FechaEntrega == null)
            {
                app.Mensaje += "El campo de fecha de entrega no puede estar vacio";
            }
            if (this.FechaEntrega < this.FechaSolicitud)
            {
                app.Mensaje += "La fecha de entrega no puede ser antes de la de solicitud";
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
            if (this.CompraEncabezadoId == null || this.CompraEncabezadoId == Guid.Empty)
            {
                app.Mensaje += "La compra encabezado id no puede estar vacia";
            }
            /*if (string.IsNullOrEmpty(this.NumeroFactura))
            {
                app.Mensaje += "El campo de numero de factura no puede estar vacio";
            }
            if (this.ProveedorId == null || this.ProveedorId == Guid.Empty)
            {
                app.Mensaje += ", el proveedor id no puede estar vacio";
            }*/
            if (this.CompraEncabezado.FechaSolicitud == null)
            {
                app.Mensaje += "El campo de fecha de solicitud no puede estar vacio";
            }
            if (this.CompraEncabezado.FechaEntrega == null)
            {
                app.Mensaje += "El campo de fecha de entrega no puede estar vacio";
            }
            if (this.CompraEncabezado.FechaEntrega < this.CompraEncabezado.FechaSolicitud)
            {
                app.Mensaje += "La fecha de entrega no puede ser antes de la de solicitud";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Compra modificada correctamente";
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
                app.Mensaje = "Compra eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

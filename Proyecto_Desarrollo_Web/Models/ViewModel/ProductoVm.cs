using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ProductoVm
    {
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public decimal Precio { get; set; }
        public Guid ProveedorId { get; set; }
        public ProveedorVm Proveedor { get; set; }
        public Guid CategoriaId { get; set; }
        public CategoriaVm Categoria { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El campo de nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Cantidad))
            {
                app.Mensaje += "El campo de cantidad no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Producto ingresado correctamente";
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
            if (this.ProductoId == null || this.ProductoId == Guid.Empty)
            {
                app.Mensaje += "El proveedor id no puede estar vacia";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El campo de nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Cantidad))
            {
                app.Mensaje += "El campo de cantidad no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Producto modificado correctamente";
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

            if (this.ProductoId == null || this.ProductoId == Guid.Empty)
            {
                app.Mensaje += "El campo producto id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Producto eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

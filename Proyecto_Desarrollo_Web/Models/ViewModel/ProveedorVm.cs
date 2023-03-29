using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ProveedorVm
    {
        public Guid ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El campo de nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Telefono))
            {
                app.Mensaje += "El campo de telefono no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Proveedor ingresado correctamente";
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
            if (this.ProveedorId == null || this.ProveedorId == Guid.Empty)
            {
                app.Mensaje += ", El proveedor id no puede estar vacia";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El campo de nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Telefono))
            {
                app.Mensaje += "El campo de telefono no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Proveedor modificado correctamente";
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

            if (this.ProveedorId == null || this.ProveedorId == Guid.Empty)
            {
                app.Mensaje += ", el campo proveedor id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Proveedor eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

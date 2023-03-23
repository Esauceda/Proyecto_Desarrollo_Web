using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class RolVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion2 { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Descripcion2))
            {
                app.Mensaje += "El campo de descripcion2 no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Rol ingresado correctamente";
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
            if (this.Id == null || this.Id == Guid.Empty)
            {
                app.Mensaje += ", El rol id no puede estar vacia";
            }
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Descripcion2))
            {
                app.Mensaje += "El campo de descripcion2 no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Rol modificado correctamente";
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

            if (this.Id == null || this.Id == Guid.Empty)
            {
                app.Mensaje += ", el campo Rol id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Rol eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

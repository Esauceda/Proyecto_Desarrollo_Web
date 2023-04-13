using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ClienteVm
    {
        public Guid ClienteId { get; set; } 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje = "El Nombre no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Apellido))
            {
                app.Mensaje += "El Apellido no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Usuario insertado correctamente";
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
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje = "El Nombre no puede ir vacio. ";
            }
            if (this.ClienteId == null || this.ClienteId == Guid.Empty)
            {
                app.Mensaje += "El UsuarioId no puede ir vacio. ";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Usuario Modificado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
        public AppResult ValidarEliminar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (this.ClienteId == null || this.ClienteId == Guid.Empty)
            {
                app.Mensaje += "El UsuarioId no puede ir vacio. ";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Usuario Eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

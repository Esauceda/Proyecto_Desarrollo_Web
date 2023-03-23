using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class UsuarioVm
    {
        public Guid UsuarioId { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es campo obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "El usuario es campo obligatorio")]
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public Guid DepartamentoId { get; set; }
        public DepartamentoVm Departamento { get; set; }
        public Guid RolId { get; set; }
        public RolVm Rol { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<AgrupadoVm> Menu { get; set; }
        public List<SelectListItem> Rols { get; set; }
        public AppResult ValidarDatosLogin()
        {
            if (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Password))
            {
                return AppResult.NoSucces("El Username o el Password no puede ir en blanco");
            }
            return AppResult.Succes("Valido", null);
        }


        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (string.IsNullOrEmpty(this.Username))
            {
                app.Mensaje = "El Username no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                app.Mensaje += "La Password no puede ir vacio. ";
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
            if (string.IsNullOrEmpty(this.Username))
            {
                app.Mensaje = "El Username no puede ir vacio. ";
            }
            if (this.UsuarioId == null || this.UsuarioId == Guid.Empty)
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
            if (this.UsuarioId == null || this.UsuarioId == Guid.Empty)
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

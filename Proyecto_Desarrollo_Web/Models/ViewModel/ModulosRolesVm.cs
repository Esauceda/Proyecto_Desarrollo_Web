using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ModulosRolesVm
    {
        public Rol Rol { get; set; }
        public Guid RolId { get; set; }
        public Modulo Modulo { get; set; }
        public Guid ModuloId { get; set; }
        public Guid Id { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<SelectListItem> modulo { get; set; }
        public List<SelectListItem> rol { get; set; }


        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (this.Id == null || this.Id == Guid.Empty)
            {
                app.Mensaje += "El Id no puede ir vacio. ";
            }
            if (this.ModuloId == null || this.ModuloId == Guid.Empty)
            {
                app.Mensaje += "El ModuloId no puede ir vacio. ";
            }
            if (this.RolId == null || this.RolId == Guid.Empty)
            {
                app.Mensaje += "El RolId no puede ir vacio. ";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Modulos insertado correctamente";
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
                app.Mensaje += "El Id no puede ir vacio. ";
            }
            if (this.ModuloId == null || this.ModuloId == Guid.Empty)
            {
                app.Mensaje += "El ModuloId no puede ir vacio. ";
            }
            if (this.RolId == null || this.RolId == Guid.Empty)
            {
                app.Mensaje += "El RolId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Modulos insertado correctamente";
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
            if (this.Id == null || this.Id == Guid.Empty)
            {
                app.Mensaje += "El Id no puede ir vacio. ";
            }
            if (this.ModuloId == null || this.ModuloId == Guid.Empty)
            {
                app.Mensaje += "El ModuloId no puede ir vacio. ";
            }
            if (this.RolId == null || this.RolId == Guid.Empty)
            {
                app.Mensaje += "El RolId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Modulos insertado correctamente";
            }

            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}


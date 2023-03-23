using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ModuloVm
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public Guid AgrupadoModulosId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public AgrupadosModulos AgrupadoModulos { get; set; }
        public List<SelectListItem> AgrupadoModuloss { get; set; }
        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El Nombre no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Metodo))
            {
                app.Mensaje += "El Metodo no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Controller))
            {
                app.Mensaje += "El Controller no puede ir vacio. ";
            }
            if (this.AgrupadoModulosId == null || this.AgrupadoModulosId == Guid.Empty)
            {
                app.Mensaje += "El AgrupadoModulosId no puede ir vacio. ";
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
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El Nombre no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Metodo))
            {
                app.Mensaje += "El Metodo no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Controller))
            {
                app.Mensaje += "El Controller no puede ir vacio. ";
            }
            if (this.AgrupadoModulosId == null || this.AgrupadoModulosId == Guid.Empty)
            {
                app.Mensaje += "El AgrupadoModulosId no puede ir vacio. ";
            }
            if (this.Id == null || this.Id == Guid.Empty)
            {
                app.Mensaje += "El ModuloId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Modulo Actualizado correctamente";
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
                app.Mensaje += "El ModuloId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Modulo Eliminado correctamente";
            }

            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class AgrupadoVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public List<ModuloVm> Modulos { get; set; }

        public AppResult Validar()
        {
            AppResult app = new AppResult();
            app.Mensaje = "";

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Agrupado Modulo ingresado correctamente";
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
                app.Mensaje += ", El Agrupado Modulo id no puede estar vacia";
            }
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Agrupado Modulo modificado correctamente";
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
                app.Mensaje += ", el campo Agrupado Modulo id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Agrupado Modulo eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}

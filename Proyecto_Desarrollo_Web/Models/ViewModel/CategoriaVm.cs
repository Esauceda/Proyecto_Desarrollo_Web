using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class CategoriaVm
    {
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


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
            
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Categoria ingresado correctamente";
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
            if (this.CategoriaId == null || this.CategoriaId == Guid.Empty)
            {
                app.Mensaje += "El tipo categoria id no puede estar vacia";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El campo de nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "El campo de descripcion no puede estar vacio";
            }
            
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Categoria modificado correctamente";
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

            if (this.CategoriaId == null || this.CategoriaId == Guid.Empty)
            {
                app.Mensaje += "El campo categoria id no puede estar vacio";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Categoria eliminada correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }

    }
}

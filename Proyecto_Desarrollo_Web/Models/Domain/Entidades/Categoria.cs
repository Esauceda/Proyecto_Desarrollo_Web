using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set;}
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public static Categoria Create(string Nombre, string Descripcion)
        {
            return new Categoria
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                CategoriaId = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }
        public void Update(string Nombre, string Descripcion)
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

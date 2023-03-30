using System;
using System.Collections;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        public string Nombre { get;set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<Producto> Productos { get; set; }

        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public static Categoria Create(string nombre, string descripcion)
        {
            return new Categoria() { Nombre = nombre,Descripcion = descripcion, CategoriaId = Guid.NewGuid(), };
        }

        
        public void update(string nombre, string descripcion)
        {
            this.Nombre= nombre;
            this.Descripcion= descripcion;
        }


        public void Delete()
        {
            this.Eliminado = true;
        }


    }
}

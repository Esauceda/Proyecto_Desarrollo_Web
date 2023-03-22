using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Rol
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion2 { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ModulosRoles> ModulosRoles { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public Rol()
        {
            ModulosRoles = new HashSet<ModulosRoles>();
            Usuarios = new HashSet<Usuario>();
        }
    }
}

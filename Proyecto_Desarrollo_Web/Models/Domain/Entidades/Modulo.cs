using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Modulo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public AgrupadosModulos AgrupadoModulos { get; set; }
        public Guid AgrupadoModulosId { get; set; }
        public ICollection<ModulosRoles> ModulosRoles { get; set; }
        public Modulo()
        {
            ModulosRoles = new HashSet<ModulosRoles>();
        }
    }
}

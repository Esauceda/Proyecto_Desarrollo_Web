using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class AgrupadosModulos
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Modulo> Modulos { get; set; }
        public AgrupadosModulos()
        {
            Modulos = new HashSet<Modulo>();
        }
    }
}

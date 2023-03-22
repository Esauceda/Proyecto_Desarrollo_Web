using System;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Departamento
    {
        public Guid DepartamentoId { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

        public Departamento()
        {
            Usuarios = new HashSet<Usuario>();
        }

    }
}

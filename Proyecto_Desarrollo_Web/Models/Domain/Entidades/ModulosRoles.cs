using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class ModulosRoles
    {
        public Rol Rol { get; set; }
        public Guid RolId { get; set; }
        public Modulo Modulo { get; set; }
        public Guid ModuloId { get; set; }
        public Guid Id { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public static ModulosRoles Create(Guid ModuloId, Guid RolId)

        {
            return new ModulosRoles
            {
                Id = Guid.NewGuid(),
                ModuloId = ModuloId,
                RolId = RolId,
            };
        }
        public void Update(Guid ModuloId, Guid RolId)
        {

            this.ModuloId = ModuloId;
            this.RolId = RolId;
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

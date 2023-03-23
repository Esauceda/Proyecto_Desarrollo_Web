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

        public static ModulosRoles Create(Guid ModuloId, Guid RolId, bool eliminado, Guid createBy,
            DateTime createdDat)

        {
            return new ModulosRoles
            {
                Id = Guid.NewGuid(),
                ModuloId = ModuloId,
                RolId = RolId,
                Eliminado = eliminado,
                CreatedBy = createBy,
                CreatedDate = createdDat,





            };
        }
        public void Update(Guid ModuloId, Guid RolId, bool eliminado, Guid createBy,
            DateTime createdDat)
        {

            this.ModuloId = ModuloId;
            this.RolId = RolId;
            this.Eliminado = eliminado;
            this.CreatedBy = createBy;
            this.CreatedDate = createdDat;
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

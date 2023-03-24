using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class AgrupadosModulos
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Modulo> Modulos { get; set; }
        public AgrupadosModulos()
        {
            Modulos = new HashSet<Modulo>();
        }
        public static AgrupadosModulos Create(string Descripcion)
        {
            return new AgrupadosModulos
            {
                Descripcion = Descripcion,
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }
        public void Update(string Descripcion)
        {
            this.Descripcion = Descripcion;

        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

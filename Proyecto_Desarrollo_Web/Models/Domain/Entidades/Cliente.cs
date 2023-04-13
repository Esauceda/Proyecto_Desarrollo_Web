using System;


namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public static Cliente Create(string Nombre, string Apellido, string Telefono, string Correo, string DNI, string Direccion)
        {
            return new Cliente
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Telefono = Telefono,
                Correo = Correo,
                DNI = DNI,
                Direccion = Direccion,
                CreatedDate = DateTime.Now,
                ClienteId = Guid.NewGuid()
            };
        }
        public void Update(string Nombre, string Apellido, string Telefono, string Correo, string DNI, string Direccion)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Telefono = Telefono;
            this.Correo = Correo;
            this.DNI = DNI;
            this.Direccion = Direccion;
        }
        public void Delete()
        {
            this.Eliminado = true;
        }

    }
}

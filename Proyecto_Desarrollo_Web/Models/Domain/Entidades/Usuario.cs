using System;

namespace Proyecto_Desarrollo_Web.Models.Domain.Entidades
{
    public class Usuario
    {
        public Rol Rol { get; set; }
        public Guid RolId { get; set; }
        public Departamento Departmento { get; set; }
        public Guid DepartamentoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string token_recovery { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


        public static Usuario Create(string Username, string Password, string Nombre,
        string Telefono, string Correo, string DNI, string Direccion, Guid DepartamentoId, Guid RolId)
        {
            return new Usuario
            {
                Username = Username,
                Password = Password,
                Nombre = Nombre,
                Telefono = Telefono,
                Correo = Correo,
                DNI = DNI,
                Direccion = Direccion,
                DepartamentoId = DepartamentoId,
                RolId = RolId,
                UsuarioId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,

            };

        }

        public void Update(string Username, string Nombre,
        string Telefono, string Correo, string DNI, string Direccion, Guid DepartamentoId, Guid RolId)
        {
            this.Username = Username;
            this.Nombre = Nombre;
            this.Telefono = Telefono;
            this.Correo = Correo;
            this.DNI = DNI;
            this.Direccion = Direccion;
            this.DepartamentoId = DepartamentoId;
            this.RolId = RolId;
        }

        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}

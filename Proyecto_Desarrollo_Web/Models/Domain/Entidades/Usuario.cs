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
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

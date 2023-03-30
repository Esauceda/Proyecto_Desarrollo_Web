using Microsoft.EntityFrameworkCore;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using static Proyecto_Desarrollo_Web.Models.Domain.Config.EntityConfig;

namespace Proyecto_Desarrollo_Web.Models.Domain
{
    public class ProyectoDBContext : DbContext
    {
        public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<AgrupadosModulos> AgrupadoModulos { get; set; }
        public DbSet<ModulosRoles> ModulosRoles { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<CompraEncabezado> CompraEncabezado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfig).Assembly);
            modelBuilder.ApplyConfiguration(new DepartamentoConfig());
            modelBuilder.ApplyConfiguration(new RolConfig());
            modelBuilder.ApplyConfiguration(new ModuloConfig());
            modelBuilder.ApplyConfiguration(new AgrupadosModulosConfig());
            modelBuilder.ApplyConfiguration(new ModulosRolesConfig());
            modelBuilder.ApplyConfiguration(new ProveedorConfig());
            modelBuilder.ApplyConfiguration(new ProductoConfig());
            modelBuilder.ApplyConfiguration(new CategoriaConfig());
            modelBuilder.ApplyConfiguration(new CompraEncabezadoConfig());
        }
    }
}

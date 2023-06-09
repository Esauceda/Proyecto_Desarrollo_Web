﻿using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Desarrollo_Web.Models.Domain.Config
{
    public class EntityConfig
    {
        public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
        {
            public void Configure(EntityTypeBuilder<Usuario> builder)
            {
                builder.HasKey(e => e.UsuarioId);
                builder.Property(a => a.Username).HasColumnType("varchar(100)").HasColumnName("Username");
                builder.Property(a => a.Password).HasColumnType("varchar(100)").HasColumnName("Password");
                builder.Property(a => a.Nombre).HasColumnType("varchar(100)").HasColumnName("Nombre");
                builder.Property(b => b.Telefono).HasColumnType("varchar(8)").HasColumnName("Telefono");
                builder.Property(c => c.Correo).HasColumnType("varchar(100)").HasColumnName("Correo");
                builder.Property(d => d.DNI).HasColumnType("varchar(100)").HasColumnName("DNI");
                builder.Property(d => d.Direccion).HasColumnType("varchar(100)").HasColumnName("Direccion");
                builder.Property(d => d.token_recovery).HasColumnType("varchar(200)").HasColumnName("token_recovery");
            }
        }

        public class DepartamentoConfig : IEntityTypeConfiguration<Departamento>
        {
            public void Configure(EntityTypeBuilder<Departamento> builder)
            {
                builder.HasKey(e => e.DepartamentoId);
                builder.Property(s => s.Descripcion).HasColumnType("varchar(100)").HasColumnName("Descripcion");
                builder.HasMany(j => j.Usuarios).WithOne(j => j.Departmento).HasForeignKey(c => c.DepartamentoId);
            }
        }

        public class RolConfig : IEntityTypeConfiguration<Rol>
        {
            public void Configure(EntityTypeBuilder<Rol> builder)
            {
                builder.HasKey(e => e.Id);
                builder.Property(s => s.Descripcion).HasColumnType("varchar(25)").HasColumnName("Descripcion");
                builder.Property(a => a.Descripcion2).HasColumnType("varchar(25)").HasColumnName("Descripcion2");
                builder.HasMany(j => j.ModulosRoles).WithOne(j => j.Rol).HasForeignKey(c => c.RolId);
                builder.HasMany(j => j.Usuarios).WithOne(j => j.Rol).HasForeignKey(c => c.RolId);
            }
        }

        public class ModuloConfig : IEntityTypeConfiguration<Modulo>
        {
            public void Configure(EntityTypeBuilder<Modulo> builder)
            {
                builder.HasKey(e => e.Id);
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.Property(a => a.Metodo).HasColumnType("varchar(25)").HasColumnName("Metodo");
                builder.Property(a => a.Controller).HasColumnType("varchar(25)").HasColumnName("Controller");
                builder.HasMany(j => j.ModulosRoles).WithOne(j => j.Modulo).HasForeignKey(c => c.ModuloId);
            }
        }
        public class ModulosRolesConfig : IEntityTypeConfiguration<ModulosRoles>
        {
            public void Configure(EntityTypeBuilder<ModulosRoles> builder)
            {
                builder.HasKey(e => e.Id);

            }
        }
        public class AgrupadosModulosConfig : IEntityTypeConfiguration<AgrupadosModulos>
        {
            public void Configure(EntityTypeBuilder<AgrupadosModulos> builder)
            {
                builder.HasKey(e => e.Id);
                builder.Property(s => s.Descripcion).HasColumnType("varchar(25)").HasColumnName("Descripcion");
                builder.HasMany(j => j.Modulos).WithOne(j => j.AgrupadoModulos).HasForeignKey(c => c.AgrupadoModulosId);
            }
        }

        public class ProveedorConfig : IEntityTypeConfiguration<Proveedor>
        {
            public void Configure(EntityTypeBuilder<Proveedor> builder)
            {
                builder.HasKey(e => e.ProveedorId);
                builder.Property(s => s.Nombre).HasColumnType("varchar(40)").HasColumnName("Nombre");
                builder.Property(s => s.Telefono).HasColumnType("varchar(8)").HasColumnName("Telefono");
                builder.HasMany(j => j.Productos).WithOne(j => j.Proveedor).HasForeignKey(c => c.ProveedorId);
                builder.HasMany(j => j.CompraEncabezados).WithOne(j => j.Proveedor).HasForeignKey(c => c.ProveedorId);
            }
        }

        public class ProductoConfig : IEntityTypeConfiguration<Producto>
        {
            public void Configure(EntityTypeBuilder<Producto> builder)
            {
                builder.HasKey(e => e.ProductoId);
                builder.Property(s => s.Nombre).HasColumnType("varchar(40)").HasColumnName("Nombre");
                builder.Property(s => s.Descripcion).HasColumnType("varchar(100)").HasColumnName("Descripcion");
                builder.Property(s => s.Cantidad).HasColumnType("varchar(6)").HasColumnName("Cantidad");
                builder.Property(s => s.Precio).HasColumnType("decimal(8,2)").HasColumnName("Precio");
                builder.HasMany(j => j.CompraDetalles).WithOne(j => j.Producto).HasForeignKey(c => c.ProductoId).OnDelete(DeleteBehavior.NoAction);
                builder.HasMany(j => j.OrdenDetalles).WithOne(j => j.Producto).HasForeignKey(c => c.ProductoId).OnDelete(DeleteBehavior.NoAction);

            }
        }

        public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
        {
            public void Configure(EntityTypeBuilder<Categoria> builder)
            {
                builder.HasKey(e => e.CategoriaId);
                builder.Property(s => s.Nombre).HasColumnType("varchar(40)").HasColumnName("Nombre");
                builder.Property(s => s.Descripcion).HasColumnType("varchar(100)").HasColumnName("Descripcion");
                builder.HasMany(j => j.Productos).WithOne(j => j.Categoria).HasForeignKey(c => c.CategoriaId);

            }
        }

        public class CompraEncabezadoConfig : IEntityTypeConfiguration<CompraEncabezado>
        {
            public void Configure(EntityTypeBuilder<CompraEncabezado> builder)
            {
                builder.HasKey(e => e.CompraEncabezadoId);
                builder.Property(s => s.NumeroFactura).HasColumnType("varchar(50)").HasColumnName("NumeroFactura");
                builder.HasMany(j => j.CompraDetalles).WithOne(j => j.CompraEncabezado).HasForeignKey(c => c.CompraEncabezadoId);
            }
        }

        public class CompraDetalleConfig : IEntityTypeConfiguration<CompraDetalle>
        {
            public void Configure(EntityTypeBuilder<CompraDetalle> builder)
            {
                builder.HasKey(e => e.CompraDetalleId);
                builder.Property(s => s.Precio).HasColumnType("decimal(8,2)").HasColumnName("Precio");
                builder.Property(s => s.Cantidad).HasColumnType("varchar(6)").HasColumnName("Cantidad");
            }
        }

        public class ClienteConfig : IEntityTypeConfiguration<Cliente>
        {
            public void Configure(EntityTypeBuilder<Cliente> builder)
            {
                builder.HasKey(e => e.ClienteId);
                builder.Property(a => a.Nombre).HasColumnType("varchar(100)").HasColumnName("Nombre");
                builder.Property(a => a.Apellido).HasColumnType("varchar(100)").HasColumnName("Apellido");
                builder.Property(b => b.Telefono).HasColumnType("varchar(8)").HasColumnName("Telefono");
                builder.Property(c => c.Correo).HasColumnType("varchar(100)").HasColumnName("Correo");
                builder.Property(d => d.DNI).HasColumnType("varchar(100)").HasColumnName("DNI");
                builder.Property(d => d.Direccion).HasColumnType("varchar(100)").HasColumnName("Direccion");
            }
        }

        public class OrdenEncabezadoConfig : IEntityTypeConfiguration<OrdenEncabezado>
        {
            public void Configure(EntityTypeBuilder<OrdenEncabezado> builder)
            {
                builder.HasKey(e => e.OrdenEncabezadoId);
                builder.HasMany(j => j.OrdenDetalles).WithOne(j => j.OrdenEncabezado).HasForeignKey(c => c.OrdenEncabezadoId);
            }
        }

        public class OrdenDetalleConfig : IEntityTypeConfiguration<OrdenDetalle>
        {
            public void Configure(EntityTypeBuilder<OrdenDetalle> builder)
            {
                builder.HasKey(e => e.OrdenDetalleId);
                builder.Property(s => s.Precio).HasColumnType("decimal(8,2)").HasColumnName("Precio");
                builder.Property(s => s.Cantidad).HasColumnType("varchar(6)").HasColumnName("Cantidad");
            }
        }
    }
}

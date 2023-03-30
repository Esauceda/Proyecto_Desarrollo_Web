﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proyecto_Desarrollo_Web.Models.Domain;

namespace Proyecto_Desarrollo_Web.Migrations
{
    [DbContext(typeof(ProyectoDBContext))]
    partial class ProyectoDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.AgrupadosModulos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("AgrupadoModulos");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Categoria", b =>
                {
                    b.Property<Guid>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Nombre");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.CompraEncabezado", b =>
                {
                    b.Property<Guid>("CompraEncabezadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSolicitud")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroFactura")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NumeroFactura");

                    b.Property<Guid>("ProveedorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompraEncabezadoId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("CompraEncabezado");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Departamento", b =>
                {
                    b.Property<Guid>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.HasKey("DepartamentoId");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Modulo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgrupadoModulosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Controller")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Controller");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Metodo")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Metodo");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("AgrupadoModulosId");

                    b.ToTable("Modulo");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.ModulosRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<Guid>("ModuloId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ModuloId");

                    b.HasIndex("RolId");

                    b.ToTable("ModulosRoles");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Producto", b =>
                {
                    b.Property<Guid>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cantidad")
                        .HasColumnType("varchar(6)")
                        .HasColumnName("Cantidad");

                    b.Property<Guid?>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Nombre");

                    b.Property<Guid>("ProveedorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductoId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Proveedor", b =>
                {
                    b.Property<Guid>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Nombre");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(8)")
                        .HasColumnName("Telefono");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Descripcion");

                    b.Property<string>("Descripcion2")
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Descripcion2");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Correo")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Correo");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DNI")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("DNI");

                    b.Property<Guid>("DepartamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Direccion");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Nombre");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Password");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(8)")
                        .HasColumnName("Telefono");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Username");

                    b.Property<string>("token_recovery")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("token_recovery");

                    b.HasKey("UsuarioId");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.CompraEncabezado", b =>
                {
                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Modulo", b =>
                {
                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.AgrupadosModulos", "AgrupadoModulos")
                        .WithMany("Modulos")
                        .HasForeignKey("AgrupadoModulosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgrupadoModulos");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.ModulosRoles", b =>
                {
                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Modulo", "Modulo")
                        .WithMany("ModulosRoles")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Rol", "Rol")
                        .WithMany("ModulosRoles")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Producto", b =>
                {
                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Categoria", "Categoria")
                        .WithMany("productos")
                        .HasForeignKey("CategoriaId");

                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Proveedor", "Proveedor")
                        .WithMany("Productos")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Usuario", b =>
                {
                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Departamento", "Departmento")
                        .WithMany("Usuarios")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departmento");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.AgrupadosModulos", b =>
                {
                    b.Navigation("Modulos");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Categoria", b =>
                {
                    b.Navigation("productos");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Departamento", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Modulo", b =>
                {
                    b.Navigation("ModulosRoles");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Proveedor", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Proyecto_Desarrollo_Web.Models.Domain.Entidades.Rol", b =>
                {
                    b.Navigation("ModulosRoles");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}

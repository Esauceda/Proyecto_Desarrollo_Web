using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private ProyectoDBContext _context;

        public UsuarioController(ILogger<UsuarioController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Index()
        {
            var ListaUsuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            return View(ListaUsuarios);
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Insertar()
        {
            var usuario = new UsuarioVm();
            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsRoles = listaRoles.ConvertAll(t => {

                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            usuario.Rols = itemsRoles;

            var listaDepartamentos = _context.Departamento.Where(w => w.Eliminado == false).ProjectToType<DepartamentoVm>().ToList();
            List<SelectListItem> itemsDepartamentos = listaDepartamentos.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.DepartamentoId.ToString(),
                    Selected = false
                };
            });
            usuario.Departamentos = itemsDepartamentos;

            return View(usuario);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Insertar(UsuarioVm usuario)
        {
            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsRoles = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            usuario.Rols = itemsRoles;

            var validacion = usuario.Validar();



            var listaDepartamentos = _context.Departamento.Where(w => w.Eliminado == false).ProjectToType<DepartamentoVm>().ToList();
            List<SelectListItem> itemsDepartamentos = listaDepartamentos.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.DepartamentoId.ToString(),
                    Selected = false
                };
            });
            usuario.Departamentos = itemsDepartamentos;

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(usuario);
            }

            var newEntidadUsuario = Usuario.Create(
                usuario.Username,
                Utilidades.Utilidades.GetMD5(usuario.Password),
                usuario.Nombre,
                usuario.Telefono,
                usuario.Correo,
                usuario.DNI,
                usuario.Direccion,
                usuario.DepartamentoId,
                usuario.RolId
            );

            _context.Usuario.Add(newEntidadUsuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Editar(Guid UsuarioId)
        {
            var usuario = _context.Usuario.Where(w => w.UsuarioId == UsuarioId && w.Eliminado == false).ProjectToType<UsuarioVm>().FirstOrDefault();
            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsRoles = listaRoles.ConvertAll(t => {

                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            usuario.Rols = itemsRoles;

            var listaDepartamentos = _context.Departamento.Where(w => w.Eliminado == false).ProjectToType<DepartamentoVm>().ToList();
            List<SelectListItem> itemsDepartamentos = listaDepartamentos.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.DepartamentoId.ToString(),
                    Selected = false
                };
            });
            usuario.Departamentos = itemsDepartamentos;

            return View(usuario);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Editar(UsuarioVm usuario)
        {
            var listaRoles = _context.Departamento.Where(w => w.Eliminado == false).ProjectToType<DepartamentoVm>().ToList();
            List<SelectListItem> itemsRoles = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.DepartamentoId.ToString(),
                    Selected = false
                };
            });
            usuario.Rols = itemsRoles;

            var listaDepartamentos = _context.Departamento.Where(w => w.Eliminado == false).ProjectToType<DepartamentoVm>().ToList();
            List<SelectListItem> itemsDepartamentos = listaDepartamentos.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.DepartamentoId.ToString(),
                    Selected = false
                };
            });
            usuario.Departamentos = itemsDepartamentos;

            var validacion = usuario.ValidarUpdate();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(usuario);
            }

            var usuarioActual = _context.Usuario.FirstOrDefault(w => w.UsuarioId == usuario.UsuarioId);
            usuarioActual.Update(
                usuario.Username,
                usuario.Nombre,
                usuario.Telefono,
                usuario.Correo,
                usuario.DNI,
                usuario.Direccion,
                usuario.DepartamentoId,
                usuario.RolId
            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Eliminar(Guid UsuarioId)
        {
            var usuario = _context.Usuario.Where(w => w.UsuarioId == UsuarioId && w.Eliminado == false).ProjectToType<UsuarioVm>().FirstOrDefault();

            return View(usuario);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Eliminar(UsuarioVm usuario)
        {
            var validacion = usuario.ValidarEliminar();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(usuario);
            }

            var registroActual = _context.Usuario.FirstOrDefault(w => w.UsuarioId == usuario.UsuarioId);
            registroActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

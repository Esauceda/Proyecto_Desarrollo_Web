using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class ModuloRolesController : Controller
    {


        private readonly ILogger<ModuloRolesController> _logger;
        private ProyectoDBContext _context;

        public ModuloRolesController(ILogger<ModuloRolesController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Index()
        {
            var listamodulosroles = _context.ModulosRoles.Where(w => w.Eliminado == false).ProjectToType<ModulosRolesVm>().ToList();
            return View(listamodulosroles);
        }
        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Insertar()
        {
            var newModuloRoles = new ModulosRolesVm();
            var modulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsmodulos = modulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            var rols = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsrols = rols.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModuloRoles.modulo = itemsmodulos;
            newModuloRoles.rol = itemsrols;
            newModuloRoles.CreatedDate = DateTime.Today;
            return View(newModuloRoles);
        }
        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Insertar(ModulosRolesVm newModuloRoles)
        {
            var modulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsmodulos = modulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            var rols = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsrols = rols.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModuloRoles.modulo = itemsmodulos;
            newModuloRoles.rol = itemsrols;
            newModuloRoles.CreatedDate = DateTime.Today;
            var validacion = newModuloRoles.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModuloRoles);
            }
            var newentidadmodulo = ModulosRoles.Create(newModuloRoles.RolId, newModuloRoles.ModuloId);

            _context.ModulosRoles.Add(newentidadmodulo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Editar(Guid Id)
        {
            var moduloroles = _context.ModulosRoles.Where(w => w.Id == Id && w.Eliminado == false).ProjectToType<ModulosRolesVm>().FirstOrDefault();
            var modulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsmodulos = modulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            var rols = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsrols = rols.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            moduloroles.modulo = itemsmodulos;
            moduloroles.rol = itemsrols;
            return View(moduloroles);
        }
        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Editar(ModulosRolesVm newModuloRoles)
        {
            var modulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsmodulos = modulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            var rols = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsrols = rols.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModuloRoles.modulo = itemsmodulos;
            newModuloRoles.rol = itemsrols;
            newModuloRoles.CreatedDate = DateTime.Today;
            var validacion = newModuloRoles.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModuloRoles);
            }
            var moduloRolActual = _context.ModulosRoles.FirstOrDefault(w => w.Id == newModuloRoles.Id);
            moduloRolActual.Update(newModuloRoles.RolId, newModuloRoles.ModuloId);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Eliminar(Guid ModuloRolesId)
        {
            var moduloRoles = _context.ModulosRoles.Where(w => w.Id == ModuloRolesId && w.Eliminado == false).ProjectToType<ModulosRolesVm>().FirstOrDefault();
            return View(moduloRoles);

        }
        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Eliminar(ModulosRolesVm newModuloRoles)
        {
            var validacion = newModuloRoles.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModuloRoles);
            }
            var moduloRolActual = _context.ModulosRoles.FirstOrDefault(w => w.Id == newModuloRoles.Id);
            moduloRolActual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

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
            var listamodulos = _context.ModulosRoles.Where(w => w.Eliminado == false).ProjectToType<ModulosRolesVm>().ToList();
            return View(listamodulos);

        }

        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Insertar()
        {
            var newModulo = new ModulosRolesVm();
            var Modulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsagrupado = Modulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Metodo,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            var rol = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsagrupado1 = rol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.modulo = itemsagrupado;
            newModulo.modulo = itemsagrupado1;
            newModulo.CreatedDate = DateTime.Today;
            return View(newModulo);
        }

        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Insertar(ModulosRolesVm newModulo)
        {

            var ModulosRoless = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsagrupado = ModulosRoless.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Metodo,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.modulo = itemsagrupado;

            var rol = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsagrupado1 = rol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            newModulo.rol = itemsagrupado1;
            newModulo.CreatedDate = DateTime.Today;
            var validacion = newModulo.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }

            var newentidadmodulo = ModulosRoles.Create(newModulo.ModuloId, newModulo.RolId,
                                                 newModulo.Eliminado, newModulo.CreatedBy, newModulo.CreatedDate);

            _context.ModulosRoles.Add(newentidadmodulo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Editar(Guid ModuloId)
        {
            var modulo = _context.ModulosRoles.Where(w => w.Id == ModuloId && w.Eliminado == false).ProjectToType<ModulosRolesVm>().FirstOrDefault();
            var ModulosRoless = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsagrupado = ModulosRoless.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Metodo,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            modulo.modulo = itemsagrupado;

            var rol = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsagrupado1 = rol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            modulo.rol = itemsagrupado;
            return View(modulo);
        }
        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Editar(ModulosRolesVm newModulo)
        {
            var ModulosRoless = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            List<SelectListItem> itemsagrupado = ModulosRoless.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Metodo,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.modulo = itemsagrupado;

            var rol = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsagrupado1 = rol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            newModulo.rol = itemsagrupado1;
            newModulo.CreatedDate = DateTime.Today;
            var validacion = newModulo.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }

            var newentidadmodulo = ModulosRoles.Create(newModulo.ModuloId, newModulo.RolId,
                                                 newModulo.Eliminado, newModulo.CreatedBy, newModulo.CreatedDate);

            _context.ModulosRoles.Add(newentidadmodulo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Eliminar(Guid ModuloI)
        {
            var modulo = _context.ModulosRoles.Where(w => w.Id == ModuloI && w.Eliminado == false).ProjectToType<ModulosRolesVm>().FirstOrDefault();
            return View(modulo);

        }


        [HttpPost]
        [ClaimRequirement("ModuloRoles")]
        public IActionResult Eliminar(ModulosRolesVm newModulo)
        {
            var validacion = newModulo.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }
            var moduloactual = _context.ModulosRoles.FirstOrDefault(w => w.Id == newModulo.Id);
            moduloactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Filters;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class ModuloController : Controller
    {
        private readonly ILogger<ModuloController> _logger;
        private ProyectoDBContext _context;

        public ModuloController(ILogger<ModuloController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Index()
        {
            var listamodulos = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            return View(listamodulos);
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Insertar()
        {
            var newModulo = new ModuloVm();
            var agrupadoModulos = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsagrupado = agrupadoModulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.AgrupadoModuloss = itemsagrupado;
            newModulo.CreatedDate = DateTime.Today;
            return View(newModulo);
        }
        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Insertar(ModuloVm newModulo)
        {
            var agrupadoModulos = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsagrupado = agrupadoModulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.AgrupadoModuloss = itemsagrupado;
            newModulo.CreatedDate = DateTime.Today;
            var validacion = newModulo.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }
            var newentidadmodulo = Modulo.Create(newModulo.Nombre, newModulo.Metodo, newModulo.Controller,
                                                 newModulo.CreatedBy, newModulo.CreatedDate, newModulo.AgrupadoModulosId);

            _context.Modulo.Add(newentidadmodulo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Editar(Guid ModuloId)
        {
            var modulo = _context.Modulo.Where(w => w.Id == ModuloId && w.Eliminado == false).ProjectToType<ModuloVm>().FirstOrDefault();
            var agrupadoModulos = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsagrupado = agrupadoModulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            modulo.AgrupadoModuloss = itemsagrupado;
            return View(modulo);
        }
        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Editar(ModuloVm newModulo)
        {
            var agrupadoModulos = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsagrupado = agrupadoModulos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newModulo.AgrupadoModuloss = itemsagrupado;
            newModulo.CreatedDate = DateTime.Today;
            var validacion = newModulo.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }
            var moduloActual = _context.Modulo.FirstOrDefault(w => w.Id == newModulo.Id);
            moduloActual.Update(newModulo.Nombre, newModulo.Metodo, newModulo.Controller,
                                                 newModulo.CreatedBy, newModulo.CreatedDate, newModulo.AgrupadoModulosId);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Eliminar(Guid ModuloId)
        {
            var modulo = _context.Modulo.Where(w => w.Id == ModuloId && w.Eliminado == false).ProjectToType<ModuloVm>().FirstOrDefault();
            return View(modulo);

        }
        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Eliminar(ModuloVm newModulo)
        {
            var validacion = newModulo.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newModulo);
            }
            var moduloactual = _context.Modulo.FirstOrDefault(w => w.Id == newModulo.Id);
            moduloactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

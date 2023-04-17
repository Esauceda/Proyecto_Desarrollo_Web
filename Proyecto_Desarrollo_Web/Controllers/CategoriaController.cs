using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private ProyectoDBContext _context;

        public CategoriaController(ILogger<CategoriaController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Index()
        {
            var ListaProducto = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            return View(ListaProducto);
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Insertar()
        {
            var newcate = new CategoriaVm();

            return View(newcate);
        }

        [HttpPost]
        [ClaimRequirement("Categoria")]
        public IActionResult Insertar(CategoriaVm newRol)
        {
            var cate = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();

            var validacion = newRol.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newRol);
            }
            var newc = Categoria.Create(newRol.Nombre, newRol.Descripcion);
            _context.Categoria.Add(newc);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Editar(Guid CategoriaId)
        {
            var categoria = _context.Categoria.Where(w => w.CategoriaId == CategoriaId && w.Eliminado == false).ProjectToType<CategoriaVm>().FirstOrDefault();

            return View(categoria);
        }

        [HttpPost]
        [ClaimRequirement("Categoria")]
        public IActionResult Editar(CategoriaVm newCategoria)
        {
            var validacion = newCategoria.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newCategoria);
            }
            var CategoriaActual = _context.Categoria.FirstOrDefault(w => w.CategoriaId == newCategoria.CategoriaId);
            CategoriaActual.Update(newCategoria.Nombre, newCategoria.Descripcion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Eliminar(Guid CategoriaId)
        {
            var categoria = _context.Categoria.Where(w => w.CategoriaId == CategoriaId && w.Eliminado == false).ProjectToType<CategoriaVm>().FirstOrDefault();

            return View(categoria);
        }

        [HttpPost]
        [ClaimRequirement("Categoria")]
        public IActionResult Eliminar(CategoriaVm newRol)
        {
            var validacion = newRol.ValidarDelete();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newRol);
            }
            var RolActual = _context.Categoria.FirstOrDefault(w => w.CategoriaId == newRol.CategoriaId);
            RolActual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Reporte()
        {
            var ListaProducto = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            return new ViewAsPdf(ListaProducto);
        }

    }
}

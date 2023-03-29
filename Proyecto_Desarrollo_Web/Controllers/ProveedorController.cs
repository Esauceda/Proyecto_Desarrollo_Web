using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System.Collections.Generic;
using System;
using System.Linq;
using Mapster;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ILogger<ProveedorController> _logger;
        private ProyectoDBContext _context;

        public ProveedorController(ILogger<ProveedorController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Proveedor")]
        public IActionResult Index()
        {
            var ListaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            return View(ListaProveedores);
        }

        [HttpGet]
        [ClaimRequirement("Proveedor")]
        public IActionResult Insertar()
        {
            var proveedor = new ProveedorVm();

            return View(proveedor);
        }

        [HttpPost]
        [ClaimRequirement("Proveedor")]
        public IActionResult Insertar(ProveedorVm proveedor)
        {
            var proveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();

            var validacion = proveedor.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(proveedor);
            }
            var newentidadProveedor = Proveedor.Create(proveedor.Nombre, proveedor.Telefono);
            _context.Proveedor.Add(newentidadProveedor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Proveedor")]
        public IActionResult Editar(Guid ProveedorId)
        {
            var proveedor = _context.Proveedor.Where(w => w.ProveedorId == ProveedorId && w.Eliminado == false).ProjectToType<ProveedorVm>().FirstOrDefault();

            return View(proveedor);
        }

        [HttpPost]
        [ClaimRequirement("Proveedor")]
        public IActionResult Editar(ProveedorVm proveedor)
        {
            var proveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();

            var validacion = proveedor.ValidarUpdate();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(proveedor);
            }

            var proveedorActual = _context.Proveedor.FirstOrDefault(w => w.ProveedorId == proveedor.ProveedorId);
            proveedorActual.Update(
                proveedor.Nombre,
                proveedor.Telefono

            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Proveedor")]
        public IActionResult Eliminar(Guid ProveedorId)
        {
            var proveedor = _context.Proveedor.Where(w => w.ProveedorId == ProveedorId && w.Eliminado == false).ProjectToType<ProveedorVm>().FirstOrDefault();

            return View(proveedor);
        }

        [HttpPost]
        [ClaimRequirement("Proveedor")]
        public IActionResult Eliminar(ProveedorVm proveedor)
        {
            var validacion = proveedor.ValidarDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(proveedor);
            }

            var proveedorActual = _context.Proveedor.FirstOrDefault(w => w.ProveedorId == proveedor.ProveedorId);
            proveedorActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

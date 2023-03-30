using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class CompraEncabezadoController : Controller
    {
        private readonly ILogger<CompraEncabezadoController> _logger;
        private ProyectoDBContext _context;

        public CompraEncabezadoController(ILogger<CompraEncabezadoController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Index()
        {
            var ListaCompra = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();
            return View(ListaCompra);
        }
        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Insertar()
        {
            var newcompra = new CompraEncabezadoVm();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            newcompra.Proveedores = itemsProveedores;

            return View(newcompra);
        }
        [HttpPost]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Insertar(CompraEncabezadoVm newcompra)
        {
            var compra = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            newcompra.Proveedores = itemsProveedores;

            var validacion = newcompra.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }
            var newentidadCompra = CompraEncabezado.Create(newcompra.NumeroFactura, newcompra.ProveedorId, newcompra.FechaSolicitud, newcompra.FechaEntrega);
            _context.CompraEncabezado.Add(newentidadCompra);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Editar(Guid CompraEncabezadoId)
        {
            var compra = _context.CompraEncabezado.Where(w => w.CompraEncabezadoId == CompraEncabezadoId && w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().FirstOrDefault();
            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            compra.Proveedores = itemsProveedores;

            return View(compra);
        }
        [HttpPost]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Editar(CompraEncabezadoVm newcompra)
        {
            var compra = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            newcompra.Proveedores = itemsProveedores;

            var validacion = newcompra.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }

            var compraActual = _context.CompraEncabezado.FirstOrDefault(w => w.CompraEncabezadoId == newcompra.CompraEncabezadoId);
            compraActual.Update(
                newcompra.NumeroFactura,
                newcompra.ProveedorId,
                newcompra.FechaSolicitud,
                newcompra.FechaEntrega
                
            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Eliminar(Guid CompraEncabezadoId)
        {
            var compra = _context.CompraEncabezado.Where(w => w.CompraEncabezadoId == CompraEncabezadoId && w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().FirstOrDefault();

            return View(compra);
        }
        [HttpPost]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Eliminar(CompraEncabezadoVm newcompra)
        {
            var validacion = newcompra.ValidarDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }

            var compraActual = _context.CompraEncabezado.FirstOrDefault(w => w.CompraEncabezadoId == newcompra.CompraEncabezadoId);
            compraActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

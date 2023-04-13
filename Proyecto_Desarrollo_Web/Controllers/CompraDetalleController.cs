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
using System.Diagnostics;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class CompraDetalleController : Controller
    {
        private readonly ILogger<CompraDetalleController> _logger;
        private ProyectoDBContext _context;

        public CompraDetalleController(ILogger<CompraDetalleController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Index()
        {
            var ListaCompra = _context.CompraDetalle.Where(w => w.Eliminado == false).ProjectToType<CompraDetalleVM>().ToList();
            return View(ListaCompra);
        }
        [HttpGet]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Insertar()
        {
            var newcompra = new CompraDetalleVM();

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            newcompra.Productos = itemsProductos;

            var listaCompraEncabezado = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();
            List<SelectListItem> itemsCompras = listaCompraEncabezado.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.NumeroFactura.ToString(),
                    Value = t.CompraEncabezadoId.ToString(),
                    Selected = false
                };
            });
            newcompra.CompraEncabezados = itemsCompras;

            return View(newcompra);
        }
        [HttpPost]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Insertar(CompraDetalleVM newcompra)
        {
            var compra = _context.CompraDetalle.Where(w => w.Eliminado == false).ProjectToType<CompraDetalleVM>().ToList();

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            newcompra.Productos = itemsProductos;

            var listaCompraEncabezado = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();
            List<SelectListItem> itemsCompras = listaCompraEncabezado.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.NumeroFactura.ToString(),
                    Value = t.CompraEncabezadoId.ToString(),
                    Selected = false
                };
            });
            newcompra.CompraEncabezados = itemsCompras;

            var validacion = newcompra.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }
            var newentidadCompra = CompraDetalle.Create(newcompra.ProductoId, newcompra.CompraEncabezadoId, newcompra.Precio, newcompra.Cantidad);
            _context.CompraDetalle.Add(newentidadCompra);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Editar(Guid CompraDetalleId)
        {
            var newcompra = _context.CompraDetalle.Where(w => w.CompraDetalleId == CompraDetalleId && w.Eliminado == false).ProjectToType<CompraDetalleVM>().FirstOrDefault();
            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            newcompra.Productos = itemsProductos;

            var listaCompraEncabezado = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();
            List<SelectListItem> itemsCompras = listaCompraEncabezado.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.NumeroFactura.ToString(),
                    Value = t.CompraEncabezadoId.ToString(),
                    Selected = false
                };
            });
            newcompra.CompraEncabezados = itemsCompras;

            return View(newcompra);
        }
        [HttpPost]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Editar(CompraDetalleVM newcompra)
        {
            var compra = _context.CompraDetalle.Where(w => w.Eliminado == false).ProjectToType<CompraDetalleVM>().ToList();

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            newcompra.Productos = itemsProductos;

            var listaCompraEncabezado = _context.CompraEncabezado.Where(w => w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().ToList();
            List<SelectListItem> itemsCompras = listaCompraEncabezado.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.NumeroFactura.ToString(),
                    Value = t.CompraEncabezadoId.ToString(),
                    Selected = false
                };
            });
            newcompra.CompraEncabezados = itemsCompras;

            var validacion = newcompra.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }

            var compraActual = _context.CompraDetalle.FirstOrDefault(w => w.CompraDetalleId == newcompra.CompraDetalleId);
            compraActual.Update(
                newcompra.ProductoId,
                newcompra.CompraEncabezadoId,
                newcompra.Precio,
                newcompra.Cantidad

            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Eliminar(Guid CompraDetalleId)
        {
            var compra = _context.CompraDetalle.Where(w => w.CompraDetalleId == CompraDetalleId && w.Eliminado == false).ProjectToType<CompraDetalleVM>().FirstOrDefault();

            return View(compra);
        }
        [HttpPost]
        [ClaimRequirement("CompraDetalle")]
        public IActionResult Eliminar(CompraDetalleVM newcompra)
        {
            var validacion = newcompra.ValidarDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }

            var compraActual = _context.CompraDetalle.FirstOrDefault(w => w.CompraDetalleId == newcompra.CompraDetalleId);
            compraActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

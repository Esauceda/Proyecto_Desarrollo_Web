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
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private ProyectoDBContext _context;

        public ProductoController(ILogger<ProductoController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Producto")]
        public IActionResult Index()
        {
            var ListaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            return View(ListaProducto);
        }

        [HttpGet]
        [ClaimRequirement("Producto")]
        public IActionResult Insertar()
        {
            var producto = new ProductoVm();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            producto.Proveedores = itemsProveedores;

            return View(producto);
        }

        [HttpPost]
        [ClaimRequirement("Producto")]
        public IActionResult Insertar(ProductoVm producto)
        {
            var productos = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            producto.Proveedores = itemsProveedores;

            var validacion = producto.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(producto);
            }
            var newentidadProducto = Producto.Create(producto.Nombre, producto.Descripcion, producto.Cantidad, producto.Precio, producto.ProveedorId);
            _context.Producto.Add(newentidadProducto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Producto")]
        public IActionResult Editar(Guid ProductoId)
        {
            var producto = _context.Producto.Where(w => w.ProductoId == ProductoId && w.Eliminado == false).ProjectToType<ProductoVm>().FirstOrDefault();
            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            producto.Proveedores = itemsProveedores;

            return View(producto);
        }

        [HttpPost]
        [ClaimRequirement("Producto")]
        public IActionResult Editar(ProductoVm producto)
        {
            var productos = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();

            var listaProveedores = _context.Proveedor.Where(w => w.Eliminado == false).ProjectToType<ProveedorVm>().ToList();
            List<SelectListItem> itemsProveedores = listaProveedores.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProveedorId.ToString(),
                    Selected = false
                };
            });
            producto.Proveedores = itemsProveedores;

            var validacion = producto.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(producto);
            }

            var productoActual = _context.Producto.FirstOrDefault(w => w.ProductoId == producto.ProductoId);
            productoActual.Update(
                producto.Nombre,
                producto.Descripcion,
                producto.Cantidad,
                producto.Precio,
                producto.ProveedorId
            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Producto")]
        public IActionResult Eliminar(Guid ProductoId)
        {
            var producto = _context.Producto.Where(w => w.ProductoId == ProductoId && w.Eliminado == false).ProjectToType<ProductoVm>().FirstOrDefault();

            return View(producto);
        }

        [HttpPost]
        [ClaimRequirement("Proveedor")]
        public IActionResult Eliminar(ProductoVm producto)
        {
            var validacion = producto.ValidarDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(producto);
            }

            var productoActual = _context.Producto.FirstOrDefault(w => w.ProductoId == producto.ProductoId);
            productoActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

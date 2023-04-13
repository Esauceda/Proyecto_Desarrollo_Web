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
using Proyecto_Desarrollo_Web.Migrations;
using Microsoft.EntityFrameworkCore;

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

        private int CantidadProductos(Guid productoId)
        {
            var producto = _context.Producto.FirstOrDefault(cd => cd.ProductoId == productoId);
            if (producto != null)
            {
                return producto.Cantidad;
            }
            return 0;
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
            var productos = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<Producto>().ToList();
            ViewBag.Productos = new SelectList(productos, "ProductoId", "Nombre");

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

            return View(newcompra);
        }
        [HttpPost]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Insertar(CompraEncabezadoVm newcompra, Guid[] productosSeleccionados)
        {
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

            var validacion = newcompra.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newcompra);
            }

            var compraEncabezado = new CompraEncabezado
            {
                NumeroFactura = newcompra.NumeroFactura,
                FechaSolicitud = newcompra.FechaSolicitud,
                FechaEntrega = newcompra.FechaEntrega,
                ProveedorId = newcompra.ProveedorId
            };
            _context.CompraEncabezado.Add(compraEncabezado);

            var compraDetalle = new CompraDetalle();
            /*{
                ProductoId = newcompra.ProductoId,
                Precio = newcompra.Precio,
                Cantidad = newcompra.Cantidad + cantidadexistente,
                CompraEncabezadoId = compraEncabezado.CompraEncabezadoId
            };
            _context.CompraDetalle.Add(compraDetalle);*/

            foreach (var ProductoId in productosSeleccionados)
            {
                var productos = _context.Producto.Find(ProductoId);
                if (productos != null)
                {
                    var compraproducto = new CompraDetalle
                    {
                        ProductoId = productos.ProductoId,
                        Precio = newcompra.Precio,
                        Cantidad = newcompra.Cantidad,
                        CompraEncabezadoId = compraEncabezado.CompraEncabezadoId
                    };
                    _context.CompraDetalle.Add(compraproducto);
                }

                var cantidadexistente = CantidadProductos(productos.ProductoId);
                var producto = _context.Producto.FirstOrDefault(p => p.ProductoId == productos.ProductoId);
                if (producto != null)
                {
                    producto.Cantidad = newcompra.Cantidad + cantidadexistente;
                    _context.Producto.Update(producto);
                }

            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult Editar(Guid CompraEncabezadoId)
        {
            var compra = _context.CompraDetalle.Where(w => w.CompraEncabezadoId == CompraEncabezadoId && w.Eliminado == false).ProjectToType<CompraEncabezadoVm>().FirstOrDefault();
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

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            compra.Productos = itemsProductos;

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

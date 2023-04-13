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
    public class OrdenEncabezadoController : Controller
    {
        private readonly ILogger<OrdenEncabezadoController> _logger;
        private ProyectoDBContext _context;

        public OrdenEncabezadoController(ILogger<OrdenEncabezadoController> logger, ProyectoDBContext context)
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
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Index()
        {
            var ListaCompra = _context.OrdenEncabezado.Where(w => w.Eliminado == false).ProjectToType<OrdenEncabezadoVm>().ToList();
            return View(ListaCompra);
        }
        [HttpGet]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Insertar()
        {
            var neworden = new OrdenEncabezadoVm();
            var productos = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<Producto>().ToList();
            ViewBag.Productos = new SelectList(productos, "ProductoId", "Nombre");

            var listaClientes = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();
            List<SelectListItem> itemsClientes = listaClientes.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ClienteId.ToString(),
                    Selected = false
                };
            });
            neworden.Clientes = itemsClientes;

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            neworden.Productos = itemsProductos;

            return View(neworden);
        }
        [HttpPost]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Insertar(OrdenEncabezadoVm neworden, Guid[] productosSeleccionados)
        {
            var listaClientes = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();
            List<SelectListItem> itemsClientes = listaClientes.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ClienteId.ToString(),
                    Selected = false
                };
            });
            neworden.Clientes = itemsClientes;

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            neworden.Productos = itemsProductos;

            var validacion = neworden.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(neworden);
            }

            var ordenEncabezado = new OrdenEncabezado
            {
                Fecha = neworden.Fecha,
                ClienteId = neworden.ClienteId
            };
            _context.OrdenEncabezado.Add(ordenEncabezado);

            var ordenDetalle = new OrdenDetalle();

            foreach (var ProductoId in productosSeleccionados)
            {
                var productos = _context.Producto.Find(ProductoId);
                if (productos != null)
                {
                    var compraproducto = new OrdenDetalle
                    {
                        ProductoId = productos.ProductoId,
                        Precio = neworden.Precio,
                        Cantidad = neworden.Cantidad,
                        OrdenEncabezadoId = ordenEncabezado.OrdenEncabezadoId
                    };
                    _context.OrdenDetalle.Add(compraproducto);
                }

                var cantidadexistente = CantidadProductos(productos.ProductoId);
                var producto = _context.Producto.FirstOrDefault(p => p.ProductoId == productos.ProductoId);
                if (producto != null)
                {
                    producto.Cantidad = cantidadexistente - neworden.Cantidad;
                    _context.Producto.Update(producto);
                }

            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Editar(Guid OrdenEncabezadoId)
        {
            var orden = _context.OrdenDetalle.Where(w => w.OrdenEncabezadoId == OrdenEncabezadoId && w.Eliminado == false).ProjectToType<OrdenEncabezadoVm>().FirstOrDefault();
            var listaClientes = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();
            List<SelectListItem> itemsClientes = listaClientes.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ClienteId.ToString(),
                    Selected = false
                };
            });
            orden.Clientes = itemsClientes;

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            orden.Productos = itemsProductos;

            return View(orden);
        }
        [HttpPost]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Editar(OrdenEncabezadoVm neworden)
        {
            var orden = _context.OrdenEncabezado.Where(w => w.Eliminado == false).ProjectToType<OrdenEncabezadoVm>().ToList();

            var listaClientes = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();
            List<SelectListItem> itemsClientes = listaClientes.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ClienteId.ToString(),
                    Selected = false
                };
            });
            neworden.Clientes = itemsClientes;

            var listaProducto = _context.Producto.Where(w => w.Eliminado == false).ProjectToType<ProductoVm>().ToList();
            List<SelectListItem> itemsProductos = listaProducto.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.ProductoId.ToString(),
                    Selected = false
                };
            });
            neworden.Productos = itemsProductos;

            var validacion = neworden.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(neworden);
            }

            var ordenActual = _context.OrdenEncabezado.FirstOrDefault(w => w.OrdenEncabezadoId == neworden.OrdenEncabezadoId);
            ordenActual.Update(
                neworden.Fecha,
                neworden.ClienteId

            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Eliminar(Guid OrdenEncabezadoId)
        {
            var orden = _context.OrdenEncabezado.Where(w => w.OrdenEncabezadoId == OrdenEncabezadoId && w.Eliminado == false).ProjectToType<OrdenEncabezadoVm>().FirstOrDefault();

            return View(orden);
        }
        [HttpPost]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult Eliminar(OrdenEncabezadoVm neworden)
        {
            var validacion = neworden.ValidarDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(neworden);
            }

            var ordenActual = _context.OrdenEncabezado.FirstOrDefault(w => w.OrdenEncabezadoId == neworden.OrdenEncabezadoId);
            ordenActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

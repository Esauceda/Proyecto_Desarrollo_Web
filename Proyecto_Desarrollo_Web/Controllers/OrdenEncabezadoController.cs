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
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;

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
            var ListaCompra = _context.OrdenEncabezado.Where(w => w.Eliminado == false && w.Fecha == DateTime.Today).ProjectToType<OrdenEncabezadoVm>().ToList();
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
                neworden.Fecha

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

        [HttpGet]
        [ClaimRequirement("OrdenEncabezado")]
        public IActionResult BuscarOrden()
        {
            var vm = new OrdenEncabezadoVm();
            return View(vm);
        }
        [HttpPost]
        [ClaimRequirement("OrdenEncabezado")]
        public JsonResult BuscarOrden([FromForm] OrdenEncabezadoVm vm)
        {
            var orden = _context.OrdenEncabezado.Where(w => w.Eliminado == false && w.Fecha == vm.Fecha).ProjectToType<OrdenEncabezadoVm>().ToList();
            var result = orden == null ? AppResult.NoSucces("No se encontro orden en estas fechas") : AppResult.Succes("Orden existente", orden);
            return new JsonResult(Ok(result));
        }

        public ActionResult GenerarReportePDF(DateTime fecha)
        {
            DataTable tabla = ObtenerDatosReporte(fecha);

            using (var stream = new MemoryStream())
            {
                // Crear un nuevo documento PDF
                var documento = new Document();
                var writer = PdfWriter.GetInstance(documento, stream);
                documento.Open();

                // Crear una tabla en el documento y agregar las filas
                var tablaPdf = new PdfPTable(tabla.Columns.Count);
                tablaPdf.DefaultCell.Border = PdfPCell.NO_BORDER;
                tablaPdf.TotalWidth = 550f;
                tablaPdf.LockedWidth = true;

                tablaPdf.DefaultCell.BackgroundColor = new BaseColor(255, 255, 255);
                tablaPdf.DefaultCell.BorderColor = new BaseColor(0, 0, 0);
                tablaPdf.DefaultCell.Padding = 10;
                tablaPdf.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPdf.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                foreach (DataColumn columna in tabla.Columns)
                {
                    var celda = new PdfPCell(new Phrase(columna.ColumnName));
                    celda.Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER;
                    tablaPdf.AddCell(celda);
                }
                foreach (DataRow fila in tabla.Rows)
                {
                    foreach (DataColumn columna in tabla.Columns)
                    {
                        var celda = new PdfPCell(new Phrase(fila[columna].ToString()));
                        celda.Border = PdfPCell.BOTTOM_BORDER;
                        tablaPdf.AddCell(celda);
                    }
                }


                // Agregar la tabla al documento
                documento.Add(tablaPdf);
                documento.Close();

                // Devolver el contenido del documento en formato PDF
                var contenido = stream.ToArray();
                return File(contenido, "application/pdf", "Ordenes.pdf");
            }
        }

        private DataTable ObtenerDatosReporte(DateTime vm)
        {
            var datos = from p in _context.OrdenDetalle.Where(p => p.Eliminado == false && p.OrdenEncabezado.Fecha.Date == vm.Date).Include(p => p.Producto)
                        select new
                        {   
                            OrdenEncabezadoId = p.OrdenEncabezadoId,
                            Cliente = p.OrdenEncabezado.Cliente.Nombre,
                            Fecha = p.OrdenEncabezado.Fecha, 
                            Producto = p.Producto.Nombre,
                            Precio = p.Precio,
                            Cantidad = p.Cantidad

                        };

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Orden Id", typeof(string));
            tabla.Columns.Add("Cliente", typeof(string));
            tabla.Columns.Add("Fecha", typeof(DateTime));
            tabla.Columns.Add("Producto", typeof(string));
            tabla.Columns.Add("Precio", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            foreach (var d in datos)
            {
                tabla.Rows.Add(d.OrdenEncabezadoId, d.Cliente, d.Fecha, d.Producto, d.Precio, d.Cantidad);
            }

            return tabla;
        }

        public ActionResult GenerarReporteExcel(DateTime fecha)
        {
            DataTable tabla = ObtenerDatosReporte(fecha);

            using (var producto = new XLWorkbook())
            {
                var hoja = producto.Worksheets.Add(tabla, "Reporte");
                hoja.ColumnsUsed().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    producto.SaveAs(stream);
                    var contenido = stream.ToArray();
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ordenes.xlsx");
                }
            }
        }
    }
}

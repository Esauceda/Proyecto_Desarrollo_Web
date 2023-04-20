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
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.IO;

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
            var ListaCompra = _context.CompraEncabezado.Where(w => w.Eliminado == false && w.FechaEntrega == DateTime.Today).ProjectToType<CompraEncabezadoVm>().ToList();
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
                newcompra.CompraEncabezado.NumeroFactura,
                newcompra.CompraEncabezado.FechaSolicitud,
                newcompra.CompraEncabezado.FechaEntrega
                
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

        [HttpGet]
        [ClaimRequirement("CompraEncabezado")]
        public IActionResult BuscarCompra()
        {
            var vm = new CompraEncabezadoVm();
            return View(vm);
        }
        [HttpPost]
        [ClaimRequirement("CompraEncabezado")]
        public JsonResult BuscarCompra([FromForm] CompraEncabezadoVm vm)
        {
            var compra = _context.CompraEncabezado.Where(w => w.Eliminado == false && w.FechaEntrega == vm.FechaEntrega).ProjectToType<CompraEncabezadoVm>().ToList();
            var result = compra == null ? AppResult.NoSucces("No se encontro compra en estas fechas") : AppResult.Succes("Compra(s) existente", compra);
            return new JsonResult(Ok(result));
        }

        public ActionResult GenerarReportePDF(DateTime fechaSolicitud, DateTime fechaEntrega)
        {
            DataTable tabla = ObtenerDatosReporte(fechaSolicitud, fechaEntrega);

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
                return File(contenido, "application/pdf", "Compras.pdf");
            }
        }
        
        private DataTable ObtenerDatosReporte(DateTime vm, DateTime vm2)
        {

            var datos = from p in _context.CompraDetalle.Where(p => p.Eliminado == false && p.CompraEncabezado.FechaSolicitud >= vm && p.CompraEncabezado.FechaEntrega <= vm2).Include(p=> p.Producto)
                        select new
                        {
                            NumeroFactura = p.CompraEncabezado.NumeroFactura,
                            Proveedor = p.CompraEncabezado.Proveedor.Nombre,
                            FechaSolicitud = p.CompraEncabezado.FechaSolicitud,
                            FechaEntrega = p.CompraEncabezado.FechaEntrega,
                            Producto = p.Producto.Nombre,
                            Precio = p.Precio,
                            Cantidad = p.Cantidad

                        };

            DataTable tabla = new DataTable();
            tabla.Columns.Add("NumeroFactura", typeof(string));
            tabla.Columns.Add("Proveedor", typeof(string));
            tabla.Columns.Add("Fecha Solicitud", typeof(DateTime));
            tabla.Columns.Add("Fecha Entrega", typeof(DateTime));
            tabla.Columns.Add("Producto", typeof(string));
            tabla.Columns.Add("Precio", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            foreach (var d in datos)
            {
                tabla.Rows.Add(d.NumeroFactura, d.Proveedor, d.FechaSolicitud, d.FechaEntrega, d.Producto, d.Precio, d.Cantidad);
            }

            return tabla;
        }

        public ActionResult GenerarReporteExcel(DateTime fechaSolicitud, DateTime fechaEntrega)
        {
            DataTable tabla = ObtenerDatosReporte(fechaSolicitud, fechaEntrega);

            using (var producto = new XLWorkbook())
            {
                var hoja = producto.Worksheets.Add(tabla, "Reporte");
                hoja.ColumnsUsed().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    producto.SaveAs(stream);
                    var contenido = stream.ToArray();
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Compras.xlsx");
                }
            }
        }
    }
}

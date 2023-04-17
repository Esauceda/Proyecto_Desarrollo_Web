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
using System.IO;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using ClosedXML.Excel;

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

            var listaCategorias = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            List<SelectListItem> itemsCategorias = listaCategorias.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.CategoriaId.ToString(),
                    Selected = false
                };
            });
            producto.Categorias = itemsCategorias;

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

            var listaCategorias = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            List<SelectListItem> itemsCategorias = listaCategorias.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.CategoriaId.ToString(),
                    Selected = false
                };
            });
            producto.Categorias = itemsCategorias;

            var validacion = producto.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(producto);
            }
            var newentidadProducto = Producto.Create(producto.Nombre, producto.Descripcion, producto.Cantidad, producto.Precio, producto.ProveedorId, producto.CategoriaId);
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

            var listaCategorias = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            List<SelectListItem> itemsCategorias = listaCategorias.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.CategoriaId.ToString(),
                    Selected = false
                };
            });
            producto.Categorias = itemsCategorias;

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

            var listaCategorias = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            List<SelectListItem> itemsCategorias = listaCategorias.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.CategoriaId.ToString(),
                    Selected = false
                };
            });
            producto.Categorias = itemsCategorias;

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
                producto.ProveedorId,
                producto.CategoriaId
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

        public ActionResult GenerarReportePDF()
        {
            DataTable tabla = ObtenerDatosReporte();

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
                return File(contenido, "application/pdf", "Producto.pdf");
            }
        }

        private DataTable ObtenerDatosReporte()
        {
            var datos = from p in _context.Producto.Include(p => p.Proveedor).Include(p => p.Categoria)
                        select new
                        {
                            Nombre = p.Nombre,
                            Descripcion = p.Descripcion,
                            Cantidad = p.Cantidad,
                            Precio = p.Precio,
                            Proveedor = p.Proveedor.Nombre,
                            Categoria = p.Categoria.Nombre

                        };

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre", typeof(string));
            tabla.Columns.Add("Descripcion", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(string));
            tabla.Columns.Add("Precio Venta", typeof(string));
            tabla.Columns.Add("Proveedor", typeof(string));
            tabla.Columns.Add("Categoria", typeof(string));

            foreach (var d in datos)
            {
                tabla.Rows.Add(d.Nombre, d.Descripcion, d.Cantidad, d.Precio, d.Proveedor, d.Categoria);
            }

            return tabla;
        }

        public ActionResult GenerarReporteExcel()
        {
            DataTable tabla = ObtenerDatosReporte();

            using (var producto = new XLWorkbook())
            {
                var hoja = producto.Worksheets.Add(tabla, "Reporte");
                hoja.ColumnsUsed().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    producto.SaveAs(stream);
                    var contenido = stream.ToArray();
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Producto.xlsx");
                }
            }
        }
    }
}

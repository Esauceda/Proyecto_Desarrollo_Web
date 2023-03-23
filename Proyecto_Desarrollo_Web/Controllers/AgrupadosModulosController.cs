using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class AgrupadosModulosController : Controller
    {
        private readonly ILogger<AgrupadosModulosController> _logger;
        private ProyectoDBContext _context;

        public AgrupadosModulosController(ILogger<AgrupadosModulosController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Index()
        {
            var listaAgrupados = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            return View(listaAgrupados);
        }
        [HttpGet]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Insertar()
        {
            var newAgrupados = new AgrupadoVm();

            return View(newAgrupados);
        }
        [HttpPost]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Insertar(AgrupadoVm newAgrupados)
        {
            var Agrupados = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();

            var validacion = newAgrupados.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newAgrupados);
            }
            var newentidadAgrupados = AgrupadosModulos.Create(newAgrupados.Descripcion);
            _context.AgrupadoModulos.Add(newentidadAgrupados);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Editar(string Id)
        {
            var Agrupados = _context.AgrupadoModulos.Where(w => w.Id == new Guid(Id) && w.Eliminado == false).ProjectToType<AgrupadoVm>().FirstOrDefault();

            return View(Agrupados);
        }
        [HttpPost]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Editar(AgrupadoVm newAgrupados)
        {
            var validacion = newAgrupados.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newAgrupados);
            }
            var AgrupadoActual = _context.AgrupadoModulos.FirstOrDefault(w => w.Id == newAgrupados.Id);
            AgrupadoActual.Update(newAgrupados.Descripcion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Eliminar(string Id)
        {
            var AModulos = _context.AgrupadoModulos.Where(w => w.Id == new Guid(Id) && w.Eliminado == false).ProjectToType<AgrupadoVm>().FirstOrDefault();

            return View(AModulos);
        }
        [HttpPost]
        [ClaimRequirement("AgrupadosModulos")]
        public IActionResult Eliminar(AgrupadoVm newAgrupados)
        {
            var validacion = newAgrupados.ValidarDelete();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newAgrupados);
            }
            var AgrupadoActual = _context.AgrupadoModulos.FirstOrDefault(w => w.Id == newAgrupados.Id);
            AgrupadoActual.Delete();
            return RedirectToAction("Index");
        }
    }
}

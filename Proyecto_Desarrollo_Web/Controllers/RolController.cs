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
    public class RolController : Controller
    {
        private readonly ILogger<RolController> _logger;
        private ProyectoDBContext _context;

        public RolController(ILogger<RolController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Index()
        {
            var listaRol = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            return View(listaRol);
        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Insertar()
        {
            var newRol = new RolVm();

            return View(newRol);
        }
        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Insertar(RolVm newRol)
        {
            var Roles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();

            var validacion = newRol.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newRol);
            }
            var newentidadRol = Rol.Create(newRol.Descripcion, newRol.Descripcion2);
            _context.Rol.Add(newentidadRol);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Editar(string Id)
        {
            var Roles = _context.Rol.Where(w => w.Id == new Guid(Id) && w.Eliminado == false).ProjectToType<RolVm>().FirstOrDefault();

            return View(Roles);
        }
        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Editar(RolVm newRol)
        {
            var validacion = newRol.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newRol);
            }
            var RolActual = _context.Rol.FirstOrDefault(w => w.Id == newRol.Id);
            RolActual.Update(newRol.Descripcion, newRol.Descripcion2);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Eliminar(string Id)
        {
            

            return View();
        }
        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Eliminar(RolVm newRol)
        {
            var validacion = newRol.ValidarDelete();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newRol);
            }
            var RolActual = _context.Rol.FirstOrDefault(w => w.Id == newRol.Id);
            RolActual.Delete();
            return RedirectToAction("Index");
        }
    }
}

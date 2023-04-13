using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Linq;
using Mapster;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private ProyectoDBContext _context;

        public ClienteController(ILogger<ClienteController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Cliente")]
        public IActionResult Index()
        {
            var ListaCliente = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();
            return View(ListaCliente);
        }

        [HttpGet]
        [ClaimRequirement("Cliente")]
        public IActionResult Insertar()
        {
            var newcliente = new ClienteVm();

            return View(newcliente);
        }

        [HttpPost]
        [ClaimRequirement("Cliente")]
        public IActionResult Insertar(ClienteVm newCliente)
        {
            var cliente = _context.Cliente.Where(w => w.Eliminado == false).ProjectToType<ClienteVm>().ToList();

            var validacion = newCliente.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newCliente);
            }
            var newcliente = Cliente.Create(newCliente.Nombre, newCliente.Apellido, newCliente.Telefono, newCliente.Correo, newCliente.DNI, newCliente.Direccion);
            _context.Cliente.Add(newcliente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Cliente")]
        public IActionResult Editar(Guid ClienteId)
        {
            var cliente = _context.Cliente.Where(w => w.ClienteId == ClienteId && w.Eliminado == false).ProjectToType<ClienteVm>().FirstOrDefault();

            return View(cliente);
        }

        [HttpPost]
        [ClaimRequirement("Cliente")]
        public IActionResult Editar(ClienteVm newCliente)
        {
            var validacion = newCliente.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newCliente);
            }
            var ClienteActual = _context.Cliente.FirstOrDefault(w => w.ClienteId == newCliente.ClienteId);
            ClienteActual.Update(newCliente.Nombre, newCliente.Apellido, newCliente.Telefono, newCliente.Correo, newCliente.DNI, newCliente.Direccion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Cliente")]
        public IActionResult Eliminar(Guid ClienteId)
        {
            var cliente = _context.Cliente.Where(w => w.ClienteId == ClienteId && w.Eliminado == false).ProjectToType<ClienteVm>().FirstOrDefault();

            return View(cliente);
        }

        [HttpPost]
        [ClaimRequirement("Cliente")]
        public IActionResult Eliminar(ClienteVm cliente)
        {
            var validacion = cliente.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(cliente);
            }
            var clienteActual = _context.Cliente.FirstOrDefault(w => w.ClienteId == cliente.ClienteId);
            clienteActual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private ProyectoDBContext _context;

        public CategoriaController(ILogger<CategoriaController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Categoria")]
        public IActionResult Index()
        {
            var ListaProducto = _context.Categoria.Where(w => w.Eliminado == false).ProjectToType<CategoriaVm>().ToList();
            return View(ListaProducto);
        }


    }
}

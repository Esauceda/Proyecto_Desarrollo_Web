using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;
        private ProyectoDBContext _context;

        public LoginController(ILogger<LoginController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index(string Codigo)
        {
            if (!string.IsNullOrEmpty(Codigo) && Codigo == "1")
            {
                ViewBag.Error = "Sesion expirado";
                ViewBag.ClaseMensaje = "alert alert-danger alert-dismissable";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioVm vm)
        {
            var app = vm.ValidarDatosLogin();
            if (!app.IsValid)
            {
                ViewBag.Error = app.Mensaje;
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return View(new UsuarioVm());
            }
            var user = _context.Usuario.Where(w => w.Eliminado == false && w.Username == vm.Username).ProjectToType<UsuarioVm>().FirstOrDefault();
            if (user == null)
            {
                ViewBag.Error = "Username o password incorrectos";
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return View(new UsuarioVm());
            }
            var contraseñaencriptada = Utilidades.Utilidades.GetMD5(vm.Password);
            if (user.Password != contraseñaencriptada)
            {
                ViewBag.Error = "Username o password incorrectos";
                ViewBag.ClaseMensaje = "alert alert alert-danger alert-dismissable";
                return View(new UsuarioVm());
            }
            var modulosroles = _context.ModulosRoles.Where(w => w.Eliminado == false && w.RolId == user.Rol.Id).ProjectToType<ModulosRolesVm>().ToList();
            var agrupadosid = modulosroles.Select(s => s.Modulo.AgrupadoModulosId).Distinct().ToList();
            var agrupados = _context.AgrupadoModulos.Where(w => agrupadosid.Contains(w.Id)).ProjectToType<AgrupadoVm>().ToList();
            foreach (var item in agrupados)
            {
                var modulosactuales = modulosroles.Where(w => w.Modulo.AgrupadoModulosId == item.Id).Select(s => s.Modulo.Id).Distinct().ToList();
                item.Modulos = item.Modulos.Where(w => modulosactuales.Contains(w.Id)).ToList();
            }
            user.Menu = agrupados;
            var sesionjson = JsonConvert.SerializeObject(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sesionjson);
            var sesionbase64 = System.Convert.ToBase64String(plainTextBytes);
            HttpContext.Session.SetString("UsuarioObjeto", sesionbase64);
            return RedirectToAction("Index", "Home");
        }
    }
}

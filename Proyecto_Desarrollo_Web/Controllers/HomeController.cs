using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Filters;
using Proyecto_Desarrollo_Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ClaimRequirement("principal")]
        public IActionResult Index(string Codigo)
        {
            if (!string.IsNullOrEmpty(Codigo) && Codigo == "1")
            {
                ViewBag.Error = "No tiene permisos para este modulo";
                ViewBag.ClaseMensaje = "alert alert-danger alert-dismissable";
            }
            return View();
        }

        [ClaimRequirement("principal")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}

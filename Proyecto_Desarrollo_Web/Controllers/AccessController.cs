using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Desarrollo_Web.Models.Domain;
using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_Desarrollo_Web.Controllers
{
    public class AccessController : Controller
    {
        string urlDomain = "https://localhost:44345/";
        private readonly ILogger<AccessController> _logger;
        private ProyectoDBContext _context;

        public AccessController(ILogger<AccessController> logger, ProyectoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StartRecovery()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StartRecovery(RecoveryVm recovery)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(recovery);
                }

                string token = Utilidades.Utilidades.GetMD5(Guid.NewGuid().ToString());
                var user = _context.Usuario.Where(w => w.Correo == recovery.Correo).FirstOrDefault();
                if (user != null)
                {
                    user.token_recovery = token;
                    _context.SaveChanges();
                    ViewBag.Error = "Se ha enviado un link para recuperar su contraseña a su correo";
                    SendEmail(user.Correo, token);
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Recovery(string token, RecoveryPasswordVm recovery)
        {

            if(token==null || token.Trim().Equals(""))
            {
                ViewBag.Error = "Seleccione Olvidaste tu password";
                return RedirectToAction("Index", "Login");
            }
            var user = _context.Usuario.Where(w => w.Eliminado == false && w.token_recovery == recovery.token).FirstOrDefault();
            if(user == null)
            {
                ViewBag.Error = "Tu token ha expirado";
                return RedirectToAction("Index", "Login");
            }
            recovery.token = token;
            return View(recovery);
        }

        [HttpPost]
        public IActionResult Recovery(RecoveryPasswordVm recovery)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(recovery);
                }
                var user = _context.Usuario.Where(w => w.Eliminado == false && w.token_recovery == recovery.token).FirstOrDefault();
                if (user != null)
                {
                     string password = Utilidades.Utilidades.GetMD5(recovery.Password);
                     user.Password = password;
                     user.token_recovery = null;
                     _context.SaveChanges();
                     ViewBag.Error = "Tu contraseña se ha cambiado";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            ViewBag.Message = "Contraseña modificada con exito";
            return RedirectToAction("Index", "Login");
        }


        #region HELPERS
        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = /*"Aqui va el correo"*/"";
            string password = /*"Aqui va la contra de su correo"*/"";
            string url = urlDomain + "Access/Recovery/?token=" + token;

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de contraseña",
                "<p>Correo para recuperacion de contraseña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, password);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();

        }
        #endregion
    }
}

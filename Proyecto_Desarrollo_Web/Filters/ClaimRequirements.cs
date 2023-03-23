using Proyecto_Desarrollo_Web.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Proyecto_Desarrollo_Web.Models.ViewModel;
using System;
using System.Linq;

namespace Proyecto_Desarrollo_Web.Filters
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {

        public ClaimRequirementAttribute(string modulo) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new ModuloVm { Nombre = modulo } };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private UsuarioVm UsuarioObjeto;
        readonly ModuloVm _claim;

        public ClaimRequirementFilter(ModuloVm claim)
        {
            _claim = claim;

        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                string sesionBase64 = filterContext.HttpContext.Session.GetString("UsuarioObjeto");
                if (string.IsNullOrEmpty(sesionBase64))
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?Codigo=1");
                    return;
                }
                var base54EncondedBytes = System.Convert.FromBase64String(sesionBase64);
                var sesion = System.Text.Encoding.UTF8.GetString(base54EncondedBytes);
                UsuarioObjeto = JsonConvert.DeserializeObject<UsuarioVm>(sesion);
                if (UsuarioObjeto == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index?Codigo=1");
                    return;
                }
                var encontro = false;
                foreach (var item in UsuarioObjeto.Menu)
                {
                    var modusloact = item.Modulos.FirstOrDefault(w => w.Nombre.Trim().ToLower() == _claim.Nombre.ToLower());
                    encontro = modusloact != null;
                    if (encontro)
                    {
                        break;
                    }

                }
                if (!encontro && _claim.Nombre.ToLower() != "principal")
                {
                    filterContext.Result = new RedirectResult("~/Home/Index?Codigo=1");
                    return;
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Usuario/Login?Codigo=1");
            }
        }

    }

}

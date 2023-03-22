using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class ModuloVm
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public Guid AgrupadoModulosId { get; set; }
    }
}

using Proyecto_Desarrollo_Web.Models.Domain.Entidades;
using System.Collections.Generic;
using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class AgrupadoVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public List<ModuloVm> Modulos { get; set; }
    }
}

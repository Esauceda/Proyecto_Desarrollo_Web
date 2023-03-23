using System.ComponentModel.DataAnnotations;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class RecoveryVm
    {
        [EmailAddress]
        [Required]
        public string Correo { get; set; }
    }
}

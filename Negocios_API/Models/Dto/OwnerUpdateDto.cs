using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Negocios_API.Models.Dto
{
    public class OwnerUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string NombrePropietario { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Clave { get; set; }
    }
}

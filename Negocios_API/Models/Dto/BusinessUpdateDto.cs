using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Negocios_API.Models.Dto
{
    public class BusinessUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [Required]
        [MaxLength(25)]
        public string NombreNegocio { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int RUC { get; set; }
    }
}

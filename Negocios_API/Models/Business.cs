using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocios_API.Models
{
    public class Business
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        [Required]
        public string NombreNegocio { get; set; }
        public string Direccion { get; set; }
        [Required]
        public int RUC { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}

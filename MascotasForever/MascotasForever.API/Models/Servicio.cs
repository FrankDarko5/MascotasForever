using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForever.API.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}
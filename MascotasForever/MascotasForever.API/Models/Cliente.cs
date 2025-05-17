using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForever.API.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }
}

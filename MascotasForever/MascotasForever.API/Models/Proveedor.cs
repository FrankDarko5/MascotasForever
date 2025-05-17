using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MascotasForever.API.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
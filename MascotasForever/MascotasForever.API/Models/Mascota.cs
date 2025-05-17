using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForever.API.Models
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Especie { get; set; }

        [MaxLength(50)]
        public string Raza { get; set; }

        public int Edad { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}
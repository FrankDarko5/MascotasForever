using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForever.API.Models
{
    public class ProductoCita
    {
        [Key]
        public int IdProductoCita { get; set; }

        [ForeignKey("Cita")]
        public int IdCita { get; set; }
        public Cita Cita { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
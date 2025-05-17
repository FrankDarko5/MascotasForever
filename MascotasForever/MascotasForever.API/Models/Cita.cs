using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MascotasForever.API.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [ForeignKey("Mascota")]
        public int IdMascota { get; set; }
        public Mascota Mascota { get; set; }

        [ForeignKey("Servicio")]
        public int IdServicio { get; set; }
        public Servicio Servicio { get; set; }

        public ICollection<ProductoCita> ProductosUtilizados { get; set; }
    }
}
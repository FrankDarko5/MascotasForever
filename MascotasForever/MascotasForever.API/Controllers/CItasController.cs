using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MascotasForever.API.Data;
using MascotasForever.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MascotasForever.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public CitasController(MascotasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await _context.Citas
                .Include(c => c.Mascota)
                .ThenInclude(m => m.Cliente)
                .Include(c => c.Servicio)
                .Include(c => c.ProductosUtilizados)
                .ThenInclude(pc => pc.Producto)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .ThenInclude(m => m.Cliente)
                .Include(c => c.Servicio)
                .Include(c => c.ProductosUtilizados)
                .ThenInclude(pc => pc.Producto)
                .FirstOrDefaultAsync(c => c.IdCita == id);

            if (cita == null)
            {
                return NotFound();
            }

            return cita;
        }

        [HttpGet("por-mascota/{idMascota}")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitasPorMascota(int idMascota)
        {
            return await _context.Citas
                .Where(c => c.IdMascota == idMascota)
                .Include(c => c.Mascota)
                .Include(c => c.Servicio)
                .ToListAsync();
        }

        [HttpGet("por-fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitasPorFecha(DateTime fecha)
        {
            return await _context.Citas
                .Where(c => c.FechaCita.Date == fecha.Date)
                .Include(c => c.Mascota)
                .ThenInclude(m => m.Cliente)
                .Include(c => c.Servicio)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCita), new { id = cita.IdCita }, cita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.IdCita)
            {
                return BadRequest();
            }

            _context.Entry(cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{idCita}/agregar-producto")]
        public async Task<ActionResult> AgregarProductoACita(int idCita, [FromBody] ProductoCitaRequest request)
        {
            var cita = await _context.Citas.FindAsync(idCita);
            if (cita == null)
            {
                return NotFound("Cita no encontrada");
            }

            var producto = await _context.Productos.FindAsync(request.IdProducto);
            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            if (producto.Stock < request.Cantidad)
            {
                return BadRequest("No hay suficiente stock del producto");
            }

            var productoEnCita = new ProductoCita
            {
                IdCita = idCita,
                IdProducto = request.IdProducto,
                Cantidad = request.Cantidad
            };

            _context.ProductosCitas.Add(productoEnCita);

            // Actualizar stock del producto
            producto.Stock -= request.Cantidad;
            _context.Entry(producto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }

    public class ProductoCitaRequest
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
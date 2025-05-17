using Microsoft.EntityFrameworkCore;
using MascotasForever.API.Models;

namespace MascotasForever.API.Data
{
    public class MascotasDbContext : DbContext
    {
        public MascotasDbContext(DbContextOptions<MascotasDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCita> ProductosCitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisión para decimales
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2); // 18 dígitos totales, 2 decimales

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Precio)
                .HasPrecision(18, 2);

            // Configurar relaciones
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Mascotas)
                .WithOne(m => m.Cliente)
                .HasForeignKey(m => m.IdCliente);

            modelBuilder.Entity<Mascota>()
                .HasMany(m => m.Citas)
                .WithOne(c => c.Mascota)
                .HasForeignKey(c => c.IdMascota);

            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Citas)
                .WithOne(c => c.Servicio)
                .HasForeignKey(c => c.IdServicio);

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Productos)
                .WithOne(pr => pr.Proveedor)
                .HasForeignKey(pr => pr.IdProveedor);

            modelBuilder.Entity<ProductoCita>()
                .HasOne(pc => pc.Cita)
                .WithMany(c => c.ProductosUtilizados)
                .HasForeignKey(pc => pc.IdCita);

            modelBuilder.Entity<ProductoCita>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.Citas)
                .HasForeignKey(pc => pc.IdProducto);
        }
    }
}
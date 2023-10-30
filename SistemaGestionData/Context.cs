using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public class Context : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ProductoVendido> ProductosVendidos { get; set; }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS02; Database = ProyectoFinal; Trusted_Connection = True; TrustServerCertificate = True");
            }
        }

    }
}
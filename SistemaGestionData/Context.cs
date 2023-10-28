using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace SistemaGestionData
{
    public class Context : DbContext
    {
      
       public  DbSet<Producto> Productos { get; set; }
       public DbSet<Usuario> Usuarios { get; set; }
       public DbSet<Venta> Ventas { get; set; }
       public DbSet<ProductoVendido> ProductosVendidos { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}

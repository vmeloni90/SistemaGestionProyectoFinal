using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual List<ProductoVendido> ProductosVendidos { get; set; }
    }
}

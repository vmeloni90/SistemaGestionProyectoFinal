using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public virtual Producto IdProducto { get; set; }
        public int Stock { get; set; }
        public virtual Venta IdVenta { get; set; }
    }
}

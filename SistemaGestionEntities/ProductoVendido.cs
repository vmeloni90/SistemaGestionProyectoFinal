using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class ProductoVendido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        public int Stock { get; set; }

        [ForeignKey("Venta")]
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
    }
}

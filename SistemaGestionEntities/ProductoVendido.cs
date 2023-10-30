﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class ProductoVendido
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        public int Stock { get; set; }

        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
    }
}

using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.Interfaces
{
    public interface IProductoVendidoRepository
    {
        public List<ProductoVendido> MostrarProductosVendidos();

    }
}

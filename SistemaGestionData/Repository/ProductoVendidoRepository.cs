using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData.Repository
{
    public class ProductoVendidoRepository: IProductoVendidoRepository
    {
        private readonly Context context;

        public ProductoVendidoRepository(Context dbContext)
        {
            context = dbContext;
        }
        public List<ProductoVendido> MostrarProductosVendidos()
        {
            return context.ProductosVendidos
                .Include(pv => pv.Producto)
                .Include(pv => pv.Venta)
                .ToList();
        }


    }
}

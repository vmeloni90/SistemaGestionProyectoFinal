using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionData.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly Context context;
        private readonly ILogger<ProductoRepository> _logger;
        public ProductoRepository(Context dbContext, ILogger<ProductoRepository> logger)
        {
            context = dbContext;
            _logger = logger;
        }

        public void CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            context.Productos.Add(producto);
            context.SaveChanges();
        }

        public Producto ObtenerProductoPorId(int productoId)
        {
            return context.Productos.Find(productoId);
        }

        public List<Producto> GetProductos()
        {
            return context.Productos.ToList();
        }

        public void EditarProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            context.Entry(producto).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void EliminarProducto(int productoId)
        {
            try
            {
                var producto = context.Productos.Find(productoId);
                if (producto != null)
                {
                    context.Productos.Remove(producto);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Asumiendo que tienes un logger configurado
                _logger.LogError(ex, "Error al intentar eliminar el producto con ID: {productoId}", productoId);
                throw;
            }
        }

       
    }
}

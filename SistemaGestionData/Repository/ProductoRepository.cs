using Microsoft.EntityFrameworkCore;
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

        public ProductoRepository(Context dbContext)
        {
            context = dbContext;
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
            var producto = context.Productos.Find(productoId);
            if (producto != null)
            {
                context.Productos.Remove(producto);
                context.SaveChanges();
            }
        }

       
    }
}

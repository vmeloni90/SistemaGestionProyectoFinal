using SistemaGestionBussiness.Interfaces;
using SistemaGestionData.Interfaces;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;

namespace SistemaGestionBussiness.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public void CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            
            if (string.IsNullOrEmpty(producto.Descripcion) || producto.Descripcion.Length > 255)
            {
                throw new ArgumentException("La descripción del producto es inválida o demasiado larga.");
            }

            
            if (producto.Costo < 0)
            {
                throw new ArgumentException("El costo del producto no puede ser negativo.");
            }

          
            if (producto.PrecioVenta < 0 || producto.PrecioVenta < producto.Costo)
            {
                throw new ArgumentException("El precio de venta del producto no puede ser negativo ni menor al costo del producto.");
            }

        
            if (producto.Stock < 0)
            {
                throw new ArgumentException("El stock del producto no puede ser negativo.");
            }

          
            if (producto.UsuarioId == null)
            {
                throw new ArgumentException("El producto debe estar asociado a un usuario.");
            }

            _productoRepository.CreateProducto(producto);
        }


        public Producto ObtenerProductoPorId(int productoId)
        {
            return _productoRepository.ObtenerProductoPorId(productoId);
        }

        public List<Producto> GetProductos()
        {
            return _productoRepository.GetProductos();
        }

        public void EditarProducto(Producto productoActualizado)
        {
            if (productoActualizado == null)
            {
                throw new ArgumentNullException(nameof(productoActualizado));
            }

            var productoEnBD = _productoRepository.ObtenerProductoPorId(productoActualizado.Id);

            if (productoEnBD == null)
            {
                throw new InvalidOperationException("El producto no existe en la base de datos.");
            }

           
            if (string.IsNullOrEmpty(productoActualizado.Descripcion) || productoActualizado.Descripcion.Length > 255)
            {
                throw new ArgumentException("La descripción del producto es inválida o demasiado larga.");
            }

            
            if (productoActualizado.Costo < 0)
            {
                throw new ArgumentException("El costo del producto no puede ser negativo.");
            }

            
            if (productoActualizado.PrecioVenta < 0 || productoActualizado.PrecioVenta < productoActualizado.Costo)
            {
                throw new ArgumentException("El precio de venta del producto no puede ser negativo ni menor al costo del producto.");
            }

          
            if (productoActualizado.Stock < 0)
            {
                throw new ArgumentException("El stock del producto no puede ser negativo.");
            }

           
            if (productoActualizado.UsuarioId == null)
            {
                throw new ArgumentException("El producto debe estar asociado a un usuario.");
            }

            
            productoEnBD.Descripcion = productoActualizado.Descripcion;
            productoEnBD.Costo = productoActualizado.Costo;
            productoEnBD.PrecioVenta = productoActualizado.PrecioVenta;
            productoEnBD.Stock = productoActualizado.Stock;
            productoEnBD.UsuarioId = productoActualizado.UsuarioId;

            _productoRepository.EditarProducto(productoEnBD);
        }


        public void EliminarProducto(int productoId)
        {
            if (productoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productoId), "El ID del producto es inválido.");
            }

            _productoRepository.EliminarProducto(productoId);
        }

        
    }
}

using SistemaGestionEntities;
using System;
using System.Collections.Generic;

namespace SistemaGestionData.Interfaces
{
    public interface IProductoRepository
    {
        void CreateProducto(Producto producto);
        Producto ObtenerProductoPorId(int productoId);
        List<Producto> GetProductos();
        void EditarProducto(Producto producto);
        void EliminarProducto(int productoId);

        
    }
}

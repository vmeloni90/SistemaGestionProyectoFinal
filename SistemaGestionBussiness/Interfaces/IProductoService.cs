﻿using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SistemaGestionBussiness.Interfaces
{
    public interface IProductoService
    {
        void CreateProducto(Producto producto);
        Producto ObtenerProductoPorId(int productoId);
        void EditarProducto(Producto producto);
        void EliminarProducto(int productoId);
        List<Producto> GetProductos();

       
    }
}

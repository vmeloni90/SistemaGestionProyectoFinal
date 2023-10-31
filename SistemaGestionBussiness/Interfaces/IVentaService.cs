﻿using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Interfaces
{
    public interface IVentaService
    {
        public void CargarVenta(Venta venta);
        public Venta ObtenerVentaPorId(int ventaId);
        public List<Venta> MostrarVentas();
        public void EditarVenta(Venta venta);
        public void EliminarVenta(int ventaId);
    }
}

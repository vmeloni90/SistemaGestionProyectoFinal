using SistemaGestionEntities;
using SistemaGestionData.Repository;
using System;
using System.Collections.Generic;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionData.Interfaces;

namespace SistemaGestionServices
{
    public class VentaService: IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository ?? throw new ArgumentNullException(nameof(ventaRepository));
        }

       
        public void CargarVenta(Venta venta)
        {
            if (venta == null)
                throw new ArgumentException("La venta no puede ser nula.");

            if (venta.ProductosVendidos == null || venta.ProductosVendidos.Count == 0)
                throw new ArgumentException("La venta debe tener al menos un producto.");

            
            _ventaRepository.CargarVenta(venta);
        }

        
        public Venta ObtenerVentaPorId(int ventaId)
        {
            if (ventaId <= 0)
                throw new ArgumentException("ID de venta inválido.");

            return _ventaRepository.ObtenerVentaPorId(ventaId);
        }

        
        public List<Venta> MostrarVentas()
        {
            return _ventaRepository.MostrarVentas();
        }

        
        public void EditarVenta(Venta venta)
        {
            if (venta == null)
                throw new ArgumentException("La venta no puede ser nula.");

            if (venta.Id <= 0)
                throw new ArgumentException("ID de venta inválido.");

            
            _ventaRepository.EditarVenta(venta);
        }

       
        public void EliminarVenta(int ventaId)
        {
            if (ventaId <= 0)
                throw new ArgumentException("ID de venta inválido.");

            _ventaRepository.EliminarVenta(ventaId);
        }
    }
}

using SistemaGestionEntities;
using SistemaGestionData.Repository;
using System;
using System.Collections.Generic;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionData.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace SistemaGestionServices
{
    public class VentaService: IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoVendidoRepository _productoVendidoRepository;


        public VentaService(IVentaRepository ventaRepository, IProductoVendidoRepository productoVendidoRepository)
        {
            _ventaRepository = ventaRepository ?? throw new ArgumentNullException(nameof(ventaRepository));
            _productoVendidoRepository = productoVendidoRepository;
        }

       
        public void CargarVenta(List<int> productos, int cantidad, string comentario, int usuario)
        {
            Venta venta = new Venta();
            venta.UsuarioId = usuario;
            venta.Comentarios = comentario;

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
            var list = _ventaRepository.MostrarVentas();            
            return list;
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

        public List<ProductoVendido> MostrarProductosVendidos()
        {
            var list = _productoVendidoRepository.MostrarProductosVendidos();
            return list;
        }
    }
}

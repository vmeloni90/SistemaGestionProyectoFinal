using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;
using SistemaGestionProyectoFinal.Views.Model;
using SistemaGestionServices;

namespace SistemaGestionWeb.Controllers
{
   
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;
        private readonly IProductoService _productoService;
        
        public VentaController(IVentaService ventaService, IProductoService productoService)
        {
            _ventaService = ventaService ?? throw new ArgumentNullException(nameof(ventaService));
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
        }

        [HttpGet]
        public IActionResult ListarVentas()
        {
            var ventas = _ventaService.MostrarVentas();
            return View(ventas);
        }

        [HttpGet]
        public IActionResult CargarVenta()
        {
            var viewModel = new CargarVentaViewModel
            {

                ProductosDisponibles = _productoService.GetProductos() 
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ListarProductosVendidos()
        {
            var productosVendidos = _ventaService.MostrarProductosVendidos();
            return View(productosVendidos);
        }

        [HttpPost]
        public IActionResult CargarVenta(CargarVentaViewModel viewModel)
        {
            _ventaService.CargarVenta(viewModel.ProductosSeleccionados, viewModel.CantidadVendida, viewModel.Comentarios, viewModel.UsuarioId);
            return RedirectToAction(nameof(ListarVentas));
        }




        [HttpPost]
        public IActionResult EditarVenta(Venta venta)
        {
            if (venta == null)
                return BadRequest("Venta data is null.");

            var ventaExistente = _ventaService.ObtenerVentaPorId(venta.Id);
            if (ventaExistente == null)
                return NotFound();

            _ventaService.EditarVenta(venta);
            return RedirectToAction("ListarVentas"); 
        }

        [HttpGet]
        public IActionResult EditarVenta(int id)
        {
            var venta = _ventaService.ObtenerVentaPorId(id);
            if (venta == null)
                return NotFound();

            return View(venta);
        }

        [HttpPost]
        public IActionResult EliminarVenta(int id)
        {
            var venta = _ventaService.ObtenerVentaPorId(id);
            if (venta == null)
                return NotFound();

            _ventaService.EliminarVenta(id);
            return RedirectToAction("ListarVentas");
        }
    }
}

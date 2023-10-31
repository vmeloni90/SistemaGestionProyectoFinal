using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionEntities;
using SistemaGestionServices;

namespace SistemaGestionWeb.Controllers
{
   
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;
        
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService ?? throw new ArgumentNullException(nameof(ventaService));
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
            return View();
        }

        [HttpPost]
        public IActionResult CargarVenta(Venta venta)
        {
            if (venta == null)
                return BadRequest("Venta data is null.");

            _ventaService.CargarVenta(venta);
            return RedirectToAction(nameof(ListarVentas));
        }

        [HttpGet("api/ventas")]
        public IActionResult Get()
        {
            var ventas = _ventaService.MostrarVentas();
            return Ok(ventas);
        }

        [HttpGet("api/ventas/{id}")]
        public IActionResult Get(int id)
        {
            var venta = _ventaService.ObtenerVentaPorId(id);

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        [HttpPost("api/ventas")]
        public IActionResult Post([FromBody] Venta venta)
        {
            if (venta == null)
                return BadRequest("Venta data is null.");

            _ventaService.CargarVenta(venta);
            return CreatedAtAction(nameof(Get), new { id = venta.Id }, venta);
        }

        [HttpPut("api/ventas/{id}")]
        public IActionResult Put(int id, [FromBody] Venta venta)
        {
            if (venta == null)
                return BadRequest("Venta data is null.");

            var ventaExistente = _ventaService.ObtenerVentaPorId(id);
            if (ventaExistente == null)
                return NotFound();

            _ventaService.EditarVenta(venta);
            return NoContent();
        }

        [HttpDelete("api/ventas/{id}")]
        public IActionResult Delete(int id)
        {
            var venta = _ventaService.ObtenerVentaPorId(id);
            if (venta == null)
                return NotFound();

            _ventaService.EliminarVenta(id);
            return NoContent();
        }
    }
}

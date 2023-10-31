using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionEntities;
using System.Collections.Generic;
using System.Security.Claims;

namespace SistemaGestionProyectoFinal.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IProductoService _productoService; 

        public ProductoController(ILogger<ProductoController> logger, IProductoService productoService)
        {
            _logger = logger;
            _productoService = productoService;
        }

        [HttpGet]
        public IActionResult ListarProductos()
        {
            var productos = _productoService.GetProductos();
            return View(productos);
        }

        [HttpGet(Name = "GetProducto")]
        public IEnumerable<Producto> Get()
        {
            return _productoService.GetProductos();
        }

        [HttpPost("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _productoService.EliminarProducto(Id);
                return RedirectToAction("ListarProductos", "Producto");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con Id: {Id}", Id);
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }


        [HttpGet]
        public IActionResult CreateProducto()
        {
            return View(new Producto());
        }

        [HttpPost]
        public IActionResult CreateProducto(Producto producto)
        {
            if (producto == null)
            {
                ViewBag.Message = "El producto no puede ser nulo.";
                return View("CreateProducto");
            }

            if (ModelState.IsValid)
            {
                _productoService.CreateProducto(producto);

                var listaDeProductos = _productoService.GetProductos();
                ViewBag.Message = "¡Producto creado con éxito!";
                return View("ListarProductos", listaDeProductos);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al crear el producto.";
                return View("CreateProducto");
            }
        }

        [HttpGet]
        public IActionResult EditarProducto(int id)
        {
            var producto = _productoService.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View("EditarProducto", producto);
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto productoActualizado)
        {
            if (productoActualizado == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productoService.EditarProducto(productoActualizado);
                    return RedirectToAction("ListarProductos");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("", "El producto proporcionado no puede ser nulo.");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el producto.");
                }
            }

            
            return View("EditarProducto", productoActualizado);
        }
    }
}

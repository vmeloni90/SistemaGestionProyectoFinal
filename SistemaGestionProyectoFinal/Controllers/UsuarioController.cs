using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionEntities;

namespace SistemaGestionProyectoFinal.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("api/[controller]")]
        [ApiController]
        public class UsuariosController : ControllerBase
        {
            private readonly IUsuarioService _usuarioServices;

            public UsuariosController(IUsuarioService usuarioServices)
            {
                _usuarioServices = usuarioServices;
            }

            [HttpGet(Name = "GetUsuario")]
            public IEnumerable<Usuario> Get()
            {
                return _usuarioServices.GetUsuarios();
            }

            [HttpDelete("{Id}", Name = "EliminarUsuario")]
            public IActionResult Delete(int Id)
            {
                _usuarioServices.EliminarUsuario(Id);
                return Ok();
            }
            [HttpPost]
            public IActionResult Agregar([FromBody] Usuario usuario)
            {
                try
                {
                    _usuarioServices.AgregarUsuario(usuario);
                    return Ok(); // Retorna un 200 OK si todo sale bien
                }
                catch (ArgumentNullException)
                {
                    return BadRequest("El usuario no puede ser nulo.");
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message); // Retorna un 400 Bad Request si el nombre de usuario ya existe
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message); // Retorna un 500 Internal Server Error para excepciones generales
                }
            }
            [HttpPut("{id}")]
            public IActionResult Actualizar(int id, [FromBody] Usuario usuarioActualizado)
            {
                if (usuarioActualizado.Id != id)
                {
                    return BadRequest("El ID del usuario no coincide con el ID de la URL.");
                }

                try
                {
                    _usuarioServices.ActualizarUsuario(usuarioActualizado);
                    return NoContent(); // Retorna un 204 No Content si todo sale bien
                }
                catch (ArgumentNullException)
                {
                    return BadRequest("El usuario proporcionado no puede ser nulo.");
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message); // Retorna un 500 Internal Server Error para excepciones generales
                }
            }
        }
    }
}

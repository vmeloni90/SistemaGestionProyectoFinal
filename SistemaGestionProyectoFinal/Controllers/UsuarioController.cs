using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionProyectoFinal.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioServices;
        public UsuarioController(IUsuarioService usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Usuario userModel)
        {
            var userInDb = _usuarioServices.ObtenerUsuarioPorNombreUsuario(userModel.NombreUsuario);

            if (userInDb != null && userModel.Password == userInDb.Password)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("Index", userModel);
            }
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
        public IActionResult Create(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            if (ModelState.IsValid)
            {
                _usuarioServices.AgregarUsuario(usuario);

                // Asumiendo que tienes un método 'Get' que toma un ID para obtener detalles de un usuario específico.
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
            }

            return BadRequest(ModelState);
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


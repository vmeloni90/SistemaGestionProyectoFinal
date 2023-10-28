using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace SistemaGestionProyectoFinal.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioServices;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioServices)
        {
            _logger = logger;
            _usuarioServices = usuarioServices;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario userModel)
        {
            var userInDb = _usuarioServices.ObtenerUsuarioPorNombreUsuario(userModel.NombreUsuario);

            if (userInDb != null && userModel.Password == userInDb.Password)
            {
                // Autenticación exitosa, redirigir al usuario a la vista de detalles del usuario.
                return RedirectToAction("MostrarUsuario", new { id = userInDb.Id });
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("Index", userModel);
            }
        }
        [HttpGet]
        public IActionResult MostrarUsuario(int id)
        {
            // Obtener el usuario por ID y mostrar sus detalles en la vista.
            var user = _usuarioServices.ObtenerUsuarioPorId(id);
            if (user == null)
            {
                // Manejar el caso en que el usuario no se encuentra.
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult BuscarUsuario(string nombreUsuario)
        {
            var usuario = _usuarioServices.ObtenerUsuarioPorNombreUsuario(nombreUsuario);

            if (usuario == null)
            {
                ViewBag.MensajeError = "Usuario no encontrado.";
                return View("MostrarUsuario"); 
            }

            return View("MostrarUsuario", usuario);
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


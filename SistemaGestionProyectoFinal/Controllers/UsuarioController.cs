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

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _usuarioServices.GetUsuarios(); 
            return View(usuarios);
        }

        [HttpGet(Name = "GetUsuario")]
        public IEnumerable<Usuario> Get()
        {
            return _usuarioServices.GetUsuarios();
        }

        [HttpPost("Eliminar/{Id}")]
        public IActionResult Delete(int Id)
        {
            _usuarioServices.EliminarUsuario(Id);
            return RedirectToAction("ListarUsuarios", "Usuario");
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

        [HttpGet]
        public IActionResult Actualizar(int id)
        {
            var user = _usuarioServices.ObtenerUsuarioPorId(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult ActualizarPost(Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _usuarioServices.ActualizarUsuario(usuarioActualizado);
                    return RedirectToAction("ListarUsuarios");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("", "El usuario proporcionado no puede ser nulo.");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el usuario.");
                }
            }

            // Si no es válido o hay un error, devuelve a la vista de edición con los errores.
            return View("ListarUsuarios", usuarioActualizado);
        }

    }

}


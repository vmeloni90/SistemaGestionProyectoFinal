using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using SistemaGestionProyectoFinal.Models;
using System.Diagnostics;
using SistemaGestionBussiness;
using SistemaGestionData;
using SistemaGestionBussiness.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaGestionProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService; 

        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username)
        {
            var user = _usuarioService.ObtenerUsuarioPorNombreUsuario(username);

            if (user != null)
            {
                // Aquí puedes manejar lo que sucede cuando se encuentra un usuario
                ViewBag.User = user;
            }
            else
            {
                // Aquí puedes manejar lo que sucede cuando no se encuentra un usuario
                ViewBag.ErrorMessage = "Usuario no encontrado.";
            }

            return View();
        }
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Aquí deberías verificar si el hash del password ingresado coincide con el almacenado
            return enteredPassword == storedPassword; // Temporalmente para tu ejemplo
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

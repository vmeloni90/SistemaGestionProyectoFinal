using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using SistemaGestionProyectoFinal.Models;
using System.Diagnostics;
using SistemaGestionBussiness;
using SistemaGestionData;
using SistemaGestionBussiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            if (!_usuarioService.IsAuthenticated(User))
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();
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


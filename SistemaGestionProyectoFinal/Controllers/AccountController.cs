using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Interfaces;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace SistemaGestionProyectoFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name;
            await _signInManager.SignOutAsync();
            return View("~/Views/Shared/Logout.cshtml", userName);
        }


    }
}

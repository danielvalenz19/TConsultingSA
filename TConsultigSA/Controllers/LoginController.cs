using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TConsultigSA.Servicios;
using TConsultigSA.Models;

namespace TConsultigSA.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioService.GetUsuarioByNombreAsync(model.NombreUsuario);

                if (usuario != null && usuario.Contraseña == model.Contraseña)
                {
                    // Redirigir a la vista de bienvenida si las credenciales son correctas
                    return RedirectToAction("Bienvenido");
                }
                else
                {
                    // Si las credenciales son incorrectas, mostrar un mensaje de error
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                }
            }

            return View(model);
        }

        public IActionResult Bienvenido()
        {
            return View();
        }
    }
}


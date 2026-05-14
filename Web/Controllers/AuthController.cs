using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Adicionado
using SirProject.Core.Interfaces;
using SirProject.Web.DTOs;
using System.Threading.Tasks;

namespace SirProject.Web.Controllers
{
    [AllowAnonymous] // Permite que usuários não logados acessem a tela de Login
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Se o usuário já estiver logado e tentar ir para /Login, manda para a Home
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Pessoas");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var token = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
            if (token == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(loginDto);
            }

            // Store token in cookie
            Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1) // Define expiração do cookie
            });

            return RedirectToAction("Index", "Pessoas");
        }

        [HttpPost]
        [Authorize] // Apenas quem está logado pode deslogar
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }
    }
}
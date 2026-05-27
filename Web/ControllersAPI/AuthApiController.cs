using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirProject.Core.Interfaces;
using SirProject.Web.DTOs;
using System.Threading.Tasks;

namespace SirProject.Web.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // In a stateless API, logout is usually handled by the client discarding the token.
            return Ok(new { Message = "Logged out successfully" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Application.DTOs;

namespace CLTSalesSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDTO registroDto)
        {
            var usuario = await _usuarioService.RegistrarAsync(registroDto);
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _usuarioService.LoginAsync(loginDto);
            if (response == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }
            return Ok(response);
        }
    }
}

using CLTSalesSystem.Application.DTOs;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Domain.Entities;
using CLTSalesSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CLTSalesSystem.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SalesDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(SalesDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null || !BCryptNet.Verify(loginDto.Password, usuario.PasswordHash))
            {
                return null;
            }

            var token = GenerarJwtToken(usuario);

            return new AuthResponseDTO
            {
                Token = token,
                Usuario = new UsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Email = usuario.Email,
                    Role = usuario.Role
                }
            };
        }

        public async Task<UsuarioResponseDTO> RegistrarAsync(RegistroUsuarioDTO registroDto)
        {
            var usuario = new Usuario
            {
                Username = registroDto.Username,
                Email = registroDto.Email,
                PasswordHash = BCryptNet.HashPassword(registroDto.Password),
                Role = "User"
            };

            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Role = usuario.Role
            };
        }

        private string GenerarJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "SecretKeyForTestingPurposesOnly1234567890");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using CLTSalesSystem.Application.DTOs;

namespace CLTSalesSystem.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
        Task<UsuarioResponseDTO> RegistrarAsync(RegistroUsuarioDTO registroDto);
    }
}

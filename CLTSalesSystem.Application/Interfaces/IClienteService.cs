using CLTSalesSystem.Application.DTOs;

namespace CLTSalesSystem.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO?> GetByIdAsync(int id);
        Task<ClienteDTO> CreateAsync(CreateClienteDTO createDto);
        Task UpdateAsync(int id, CreateClienteDTO updateDto);
        Task DeleteAsync(int id);
    }
}

using CLTSalesSystem.Application.DTOs;

namespace CLTSalesSystem.Application.Interfaces
{
    public interface IVentaService
    {
        Task<IEnumerable<VentaDTO>> GetAllAsync();
        Task<VentaDTO?> GetByIdAsync(int id);
        Task<VentaDTO> CreateAsync(CreateVentaDTO createDto);
    }
}

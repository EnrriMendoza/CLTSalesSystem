using CLTSalesSystem.Application.DTOs;

namespace CLTSalesSystem.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO?> GetByIdAsync(int id);
        Task<ProductoDTO> CreateAsync(CreateProductoDTO createDto);
        Task UpdateAsync(int id, CreateProductoDTO updateDto);
        Task DeleteAsync(int id);
    }
}

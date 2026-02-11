using CLTSalesSystem.Application.DTOs;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Domain.Entities;
using CLTSalesSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CLTSalesSystem.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly SalesDbContext _context;

        public ProductoService(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock
                })
                .ToListAsync();
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null) return null;

            return new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            };
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoDTO createDto)
        {
            var entity = new Producto
            {
                Nombre = createDto.Nombre,
                Precio = createDto.Precio,
                Stock = createDto.Stock
            };

            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();

            return new ProductoDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Precio = entity.Precio,
                Stock = entity.Stock
            };
        }

        public async Task UpdateAsync(int id, CreateProductoDTO updateDto)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity == null) return; 

            entity.Nombre = updateDto.Nombre;
            entity.Precio = updateDto.Precio;
            entity.Stock = updateDto.Stock;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Productos.FindAsync(id);
            if (entity != null)
            {
                _context.Productos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

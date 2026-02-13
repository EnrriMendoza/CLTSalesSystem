using CLTSalesSystem.Application.DTOs;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Domain.Entities;
using CLTSalesSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CLTSalesSystem.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly SalesDbContext _context;

        public ClienteService(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            return await _context.Clientes
                .Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono
                }).ToListAsync();
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id)
        {
            var c = await _context.Clientes.FindAsync(id);
            if (c == null) return null;

            return new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono
            };
        }

        public async Task<ClienteDTO> CreateAsync(CreateClienteDTO createDto)
        {
            var cliente = new Cliente
            {
                Nombre = createDto.Nombre,
                Email = createDto.Email,
                Telefono = createDto.Telefono
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };
        }

        public async Task UpdateAsync(int id, CreateClienteDTO updateDto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Nombre = updateDto.Nombre;
                cliente.Email = updateDto.Email;
                cliente.Telefono = updateDto.Telefono;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}

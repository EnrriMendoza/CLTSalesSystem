using CLTSalesSystem.Application.DTOs;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Domain.Entities;
using CLTSalesSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CLTSalesSystem.Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly SalesDbContext _context;

        public VentaService(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Detalles)
                .ToListAsync();

            return ventas.Select(v => new VentaDTO
            {
                Id = v.Id,
                ClienteId = v.ClienteId,
                Fecha = v.Fecha,
                Total = v.Total,
                Detalles = v.Detalles.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            });
        }

        public async Task<VentaDTO?> GetByIdAsync(int id)
        {
            var v = await _context.Ventas
                .Include(x => x.Detalles)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (v == null) return null;

            return new VentaDTO
            {
                Id = v.Id,
                ClienteId = v.ClienteId,
                Fecha = v.Fecha,
                Total = v.Total,
                Detalles = v.Detalles.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }

        public async Task<VentaDTO> CreateAsync(CreateVentaDTO createDto)
        {
            var productIds = createDto.Detalles.Select(d => d.ProductoId).Distinct().ToList();
            var products = await _context.Productos
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            if (products.Count != productIds.Count)
            {
                 throw new Exception("Uno o más productos no existen.");
            }

            var nuevaVenta = new Venta
            {
                ClienteId = createDto.ClienteId,
                Fecha = DateTime.UtcNow,
                Detalles = new List<DetalleVenta>()
            };

            decimal totalVenta = 0;

            foreach (var detalleDto in createDto.Detalles)
            {
                var producto = products.First(p => p.Id == detalleDto.ProductoId);

                if (producto.Stock < detalleDto.Cantidad)
                {
                    throw new Exception($"Stock insuficiente para el producto {producto.Nombre}. Stock actual: {producto.Stock}, Solicitado: {detalleDto.Cantidad}");
                }

                producto.Stock -= detalleDto.Cantidad;

                var subtotal = producto.Precio * detalleDto.Cantidad;
                totalVenta += subtotal;

                nuevaVenta.Detalles.Add(new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = subtotal
                });
            }

            nuevaVenta.Total = totalVenta;

            _context.Ventas.Add(nuevaVenta);
            
            await _context.SaveChangesAsync(); 

            return new VentaDTO
            {
                Id = nuevaVenta.Id,
                ClienteId = nuevaVenta.ClienteId,
                Fecha = nuevaVenta.Fecha,
                Total = nuevaVenta.Total,
                Detalles = nuevaVenta.Detalles.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }
    }
}

using CLTSalesSystem.Application.DTOs;
using CLTSalesSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CLTSalesSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _ventaService.GetAllAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVentaDTO createDto)
        {
            var createdVenta = await _ventaService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdVenta.Id }, createdVenta);
        }
    }
}

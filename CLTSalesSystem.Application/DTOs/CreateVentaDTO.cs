namespace CLTSalesSystem.Application.DTOs
{
    public class CreateVentaDTO
    {
        public int ClienteId { get; set; }
        public List<CreateDetalleVentaDTO> Detalles { get; set; } = new List<CreateDetalleVentaDTO>();
    }
}

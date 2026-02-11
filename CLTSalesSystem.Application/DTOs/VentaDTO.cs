namespace CLTSalesSystem.Application.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }
}

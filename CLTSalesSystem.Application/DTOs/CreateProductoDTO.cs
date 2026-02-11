namespace CLTSalesSystem.Application.DTOs
{
    public class CreateProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}

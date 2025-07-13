namespace backend_examen.Application.DTOs
{
    public class BuyProductsResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public int Change { get; set; }
        public bool OutOfService { get; set; }
    }
}

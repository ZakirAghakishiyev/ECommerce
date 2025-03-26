namespace ECommerce.Application.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class OrderItemCreateDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }

}

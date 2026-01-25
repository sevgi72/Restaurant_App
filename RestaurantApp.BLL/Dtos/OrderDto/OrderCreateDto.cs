
namespace RestaurantApp.BLL.Dtos.OrderDto
{
    public class OrderCreateDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();
    }

    public class OrderItemCreateDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}

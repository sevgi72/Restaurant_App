namespace RestaurantApp.BLL.Dtos.OrderDto
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public List<OrderItemReturnDto> OrderItems { get; set; } = new List<OrderItemReturnDto>();
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }

    public class OrderItemReturnDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public decimal MenuItemPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;


namespace RestaurantApp.BLL.Dtos.OrderDto
{
    public class OrderUpdateDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }


    }
}

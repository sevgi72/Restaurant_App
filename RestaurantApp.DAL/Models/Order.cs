using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class Order:BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
      

    }
}

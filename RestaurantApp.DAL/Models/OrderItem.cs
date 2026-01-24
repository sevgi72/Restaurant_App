using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class OrderItem:BaseEntity
    {
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Count { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }

    }
}

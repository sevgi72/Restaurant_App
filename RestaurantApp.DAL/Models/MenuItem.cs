using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public class MenuItem:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int OrderItemId { get; set; }
        public Order Order { get; set; }
    }
}

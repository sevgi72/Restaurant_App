using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.MenuItemDto
{
    public class MenuItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}

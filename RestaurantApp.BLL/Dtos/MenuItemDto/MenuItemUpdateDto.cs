using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Dtos.MenuItemDto
{
    public class MenuItemUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

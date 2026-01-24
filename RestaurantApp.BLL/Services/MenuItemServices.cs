using RestaurantApp.BLL.Interfaces;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.BLL.Services
{
    public class MenuItemServices : IMenuItemService
    {
        private readonly List<MenuItem> _menuItems;
        private int _idCounter = 1;

        public MenuItemServices()
        {
            _menuItems = new List<MenuItem>();
        }


        public Task AddMenuItemAsync(string name, decimal price, string category)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("MenuItem adi bos ola bilmez");

            if (price <= 0)
                throw new Exception("Qiymet 0-dan boyuk olmalidir");

            var exists = _menuItems.Any(m => m.Name.ToLower() == name.ToLower());
            if (exists)
                throw new Exception("Bu adda menu item artiq movcuddur");

            var menuItem = new MenuItem
            {
                Id = _idCounter++,
                Name = name,
                Price = price,
                Category = category
            };
            _menuItems.Add(menuItem);
            return Task.CompletedTask;
        }

        public Task<List<MenuItem>> GetByCategoryAsync(string category)
        {
            if(string.IsNullOrWhiteSpace(category))
                throw new Exception("Kateqoriya adi bos ola bilmez");
            var existingItems = _menuItems
                .Where(m => m.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(existingItems);

        }

        public Task<List<MenuItem>> GetByPriceIntervalAsync(decimal min, decimal max)
        {
            if(min<0 || max < 0)
                throw new Exception("Qiymet menfi ola bilmez");
            if (min > max)
                throw new Exception("Minimum qiymet maksimum qiymetden boyuk ola bilmez");
            var existingItems = _menuItems
                .Where(m => m.Price >= min && m.Price <= max)
                .ToList();
            return Task.FromResult(existingItems);

        }

        public Task RemoveMenuItemAsync(int id)
        {
            var existingItem = _menuItems.FirstOrDefault(m => m.Id == id);
            if (existingItem == null)
                throw new Exception("Menu item tapilmadi");
            _menuItems.Remove(existingItem);
            return Task.CompletedTask;
        }

        public Task EditMenuItemAsync(int id, string newName, decimal newPrice)
        {
            var existingItem = _menuItems.FirstOrDefault(m => m.Id == id);
            if (existingItem == null)
                throw new Exception("Menu item tapilmadi");

            if (string.IsNullOrWhiteSpace(newName))
                throw new Exception("MenuItem adi bos ola bilmez");

            if (newPrice <= 0)
                throw new Exception("Qiymet 0-dan boyuk olmalidir");

            var nameExists = _menuItems.Any(m => m.Id != id && m.Name.ToLower() == newName.ToLower());
            if (nameExists)
                throw new Exception("Bu adda menu item artiq movcuddur");

            existingItem.Name = newName;
            existingItem.Price = newPrice;
            return Task.CompletedTask;
        }

        public Task<List<MenuItem>> SearchAsync(string searchText)
        {
            if(string.IsNullOrWhiteSpace(searchText))
                throw new Exception("Axtaris metni bos ola bilmez");
            return Task.FromResult(_menuItems
                .Where(m => m.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList());
        }
    }
}

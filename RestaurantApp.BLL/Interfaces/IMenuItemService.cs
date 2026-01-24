using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.DAL.Models;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(string name, decimal price, string category);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(int id, string newName, decimal newPrice);

        Task<List<MenuItem>> GetByCategoryAsync(string category);
        Task<List<MenuItem>> GetByPriceIntervalAsync(decimal min, decimal max);
        Task<List<MenuItem>> SearchAsync(string searchText);
    }

}

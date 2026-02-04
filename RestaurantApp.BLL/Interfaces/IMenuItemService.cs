using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.DAL.Models;
using RestaurantApp.BLL.Dtos.MenuItemDto;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IMenuItemService
    {
        
        Task AddMenuItemAsync(MenuItemCreateDto dto);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(int id,MenuItemUpdateDto dto);

        Task<List<MenuItemReturnDto>> GetByCategoryAsync(string category);
        Task<List<MenuItemReturnDto>> GetByPriceIntervalAsync(decimal min, decimal max);
        Task<List<MenuItemReturnDto>> SearchAsync(string searchText);
        Task<List<MenuItemReturnDto>> GetAllMenuItems();
    }

}

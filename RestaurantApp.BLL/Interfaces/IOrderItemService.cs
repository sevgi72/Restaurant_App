using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(int orderNo, int menuItemId, int quantity);

        Task RemoveOrderItemAsync(int orderNo, int menuItemId);

        Task UpdateQuantityAsync(int orderNo, int menuItemId, int newQuantity);

        Task<List<OrderItem>> GetOrderItemsAsync(int orderNo);

        Task<decimal> GetOrderTotalPriceAsync(int orderNo);
    }

}

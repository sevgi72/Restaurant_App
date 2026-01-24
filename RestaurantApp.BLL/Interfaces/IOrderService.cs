using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.DAL.Models;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(List<(int menuItemId, int quantity)> items);
        Task RemoveOrderAsync(int orderNo);

        Task<Order> GetOrderByNoAsync(int orderNo);
        Task<List<Order>> GetOrderByDateAsync(DateTime date);
        Task<List<Order>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end);
        Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal min, decimal max);
    }

}

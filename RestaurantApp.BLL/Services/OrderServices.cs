using RestaurantApp.BLL.Interfaces;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        public Task AddOrderAsync(List<(int menuItemId, int quantity)> items)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrderByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByNoAsync(int orderNo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrderAsync(int orderNo)
        {
            throw new NotImplementedException();
        }
    }

}

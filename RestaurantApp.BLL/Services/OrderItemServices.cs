using RestaurantApp.BLL.Interfaces;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.BLL.Services
{
    public class OrderItemServices : IOrderItemService
    {
        public Task AddOrderItemAsync(int orderNo, int menuItemId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderItem>> GetOrderItemsAsync(int orderNo)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetOrderTotalPriceAsync(int orderNo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrderItemAsync(int orderNo, int menuItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(int orderNo, int menuItemId, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}

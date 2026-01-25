using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.DAL.Models;
using RestaurantApp.BLL.Dtos.OrderDto;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderCreateDto dto);
        Task RemoveOrderAsync(int orderNo);
        Task<List<OrderReturnDto>> GetAllOrdersAsync();

        Task<OrderReturnDto> GetOrderByNoAsync(int orderNo);
        Task<List<OrderReturnDto>> GetOrderByDateAsync(DateTime date);
        Task<List<OrderReturnDto>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end);
        Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal min, decimal max);
    }

}

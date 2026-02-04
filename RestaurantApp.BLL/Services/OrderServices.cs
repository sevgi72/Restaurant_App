using RestaurantApp.BLL.Interfaces;
using RestaurantApp.BLL.Dtos.OrderDto;
using RestaurantApp.DAL.Models;
using RestaurantApp.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RestaurantApp.DAL.Concretes;

namespace RestaurantApp.BLL.Services
{
    public class OrderServices(Repository<Order> _orderRepo, IMapper mapper) : IOrderService
    {


        public async Task AddOrderAsync(OrderCreateDto orderCreateDto)
        {
          
            var order = mapper.Map<Order>(orderCreateDto);
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();
        }
        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            var orders=await _orderRepo.GetAll()
                .Include(o=>o.OrderItems)
                .ToListAsync();
            return mapper.Map<List<OrderReturnDto>>(orders);
        }

        public async Task<List<OrderReturnDto>> GetOrderByDateAsync(DateTime date)
        {
            if(date==null)
                throw new Exception("Tarix daxil edilmeyib");
            var ordersForDate = await _orderRepo.GetAll()
                .Where(o => o.Date.Year == date.Year && 
                           o.Date.Month == date.Month && 
                           o.Date.Day == date.Day)
                .ToListAsync();
            if(ordersForDate==null || ordersForDate.Count==0)
                throw new Exception("Bu tarixde sifaris tapilmadi");

            return mapper.Map<List<OrderReturnDto>>(ordersForDate);
        }

        public async Task<OrderReturnDto> GetOrderByNoAsync(int orderNo)
        {
            if (orderNo <= 0)
                throw new Exception("Sifaris nomresi menfi ve ya 0 ola bilmez");
            var order = await _orderRepo.GetAll()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderNo);
            
            if (order == null)
                throw new Exception("Sifaris tapilmadi");

            return mapper.Map<OrderReturnDto>(order);
        }

        public async Task<List<OrderReturnDto>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end)
        {
            if(start == null || end == null)
                throw new Exception("Tarix daxil edilmeyib");
            if (start > end)
                throw new Exception("Baslangic tarixi son tarixden bosuk olmalidir");

            var ordersInInterval = await _orderRepo.GetAll()
                .Where(o => o.Date >= start && o.Date <= end)
                .ToListAsync();
            if(ordersInInterval==null || ordersInInterval.Count==0)
                throw new Exception("Bu tarix araliginda sifaris tapilmadi");

            return mapper.Map<List<OrderReturnDto>>(ordersInInterval);
        }

        public async Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal min, decimal max)
        {
            if (min < 0 || max < 0)
                throw new Exception("Qiymet menfi ola bilmez");
            
            if (min > max)
                throw new Exception("Minimum qiymet maksimum qiymetden boyuk ola bilmez");

            var ordersInPriceRange = await _orderRepo.GetAll()
                .Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
                .Include(o => o.OrderItems)
                .ToListAsync();
            if(ordersInPriceRange==null || ordersInPriceRange.Count==0)
                throw new Exception("Bu qiymet araliginda sifaris tapilmadi");

            return mapper.Map<List<OrderReturnDto>>(ordersInPriceRange);
        }

        public async Task RemoveOrderAsync(int orderNo)
        {
            var order = await _orderRepo.GetByIdAsync( orderNo);
            if (order == null)
                throw new Exception("Sifaris tapilmadi");
            _orderRepo.RemoveAsync(order);
            await _orderRepo.SaveChangesAsync();
        }
    }
}



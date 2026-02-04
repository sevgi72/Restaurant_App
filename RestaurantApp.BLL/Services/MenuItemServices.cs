using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.BLL.Dtos.MenuItemDto;
using RestaurantApp.BLL.Interfaces;
using RestaurantApp.DAL.Concretes;
using RestaurantApp.DAL.Data;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantApp.BLL.Services
{
    public class MenuItemServices(Repository<MenuItem> _menuItemRepo, IMapper mapper) : IMenuItemService
    {
        public async Task<List<MenuItemReturnDto>> GetAllMenuItems()
        {
            var menuItems = await _menuItemRepo.GetAll()
                .ProjectTo<MenuItemReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return menuItems;
        }

        public async Task AddMenuItemAsync(MenuItemCreateDto menuItemCreateDto)
        {
            if (string.IsNullOrWhiteSpace(menuItemCreateDto.Name))
                throw new Exception("MenuItem adi bos ola bilmez");
            
            if (menuItemCreateDto.Price <= 0)
                throw new Exception("Qiymet 0-dan boyuk olmalidir");
            
            var nameExists = await _menuItemRepo
                .AnyAsync(m => m.Name.ToLower() == menuItemCreateDto.Name.ToLower());
            
            if (nameExists)
                throw new Exception("Bu adda menu item artiq movcuddur");
            
            var menuItem = mapper.Map<MenuItem>(menuItemCreateDto);
            await _menuItemRepo.AddAsync(menuItem);
            await _menuItemRepo.SaveChangesAsync();
        }

        public async Task<List<MenuItemReturnDto>> GetByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new Exception("Category bos ola bilmez");

            var itemsInCategory = await _menuItemRepo.GetByConditionAsync
                (m => m.Category.ToLower() == category.ToLower());
            if(itemsInCategory==null)
                throw new Exception("Bu category-e aid menu item tapilmadi");

            return mapper.Map<List<MenuItemReturnDto>>(itemsInCategory);
        }

        public async Task<List<MenuItemReturnDto>> GetByPriceIntervalAsync(decimal min, decimal max)
        {
            if (min < 0 || max < 0)
                throw new Exception("Qiymetler menfi ola bilmez");
            
            if (min > max)
                throw new Exception("Minimum qiymet maksimum qiymetden boyuk ola bilmez");

            var itemsInRange = await _menuItemRepo.GetByPriceIntervalAsync(
                m => m.Price,min,max);
                
            if(itemsInRange==null)
                throw new Exception("Bu qiymet araligina aid menu item tapilmadi");

            return mapper.Map<List<MenuItemReturnDto>>(itemsInRange);
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var existingItem = await _menuItemRepo.GetByIdAsync(id);
            
            if (existingItem == null)
                throw new Exception("Menu item tapilmadi");

            await _menuItemRepo.RemoveAsync(existingItem);
            await _menuItemRepo.SaveChangesAsync();
        }

        public async Task EditMenuItemAsync(int id,MenuItemUpdateDto dto)
        {
            if(id!=dto.Id)
                throw new Exception("Idler uygun deyil");

            var existingItem = await _menuItemRepo.GetByIdAsync(dto.Id);
            
            if (existingItem == null)
                throw new Exception("Menu item tapilmadi");
            
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Menu item adi bos ola bilmez");
            
            if (dto.Price <= 0)
                throw new Exception("Qiymet 0-dan boyuk olmalidir");
            
            var nameExists = await _menuItemRepo
                .AnyAsync(m => m.Name.ToLower() == dto.Name.ToLower() && m.Id != dto.Id);
            
            if (nameExists)
                throw new Exception("Bu adda menu item artiq movcuddur");

            mapper.Map(dto, existingItem);
            await _menuItemRepo.SaveChangesAsync();
        }

        public async Task<List<MenuItemReturnDto>> SearchAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new Exception("Axtaris metni bos ola bilmez");

            var searchResults = await _menuItemRepo.GetByConditionAsync(
                m => m.Name.ToLower().Contains(searchText.ToLower()) ||
                     m.Category.ToLower().Contains(searchText.ToLower())
                     );
            

            return mapper.Map<List<MenuItemReturnDto>>(searchResults);
        }
    }
}

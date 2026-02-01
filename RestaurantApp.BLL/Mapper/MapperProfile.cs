using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RestaurantApp.BLL.Dtos.MenuItemDto;
using RestaurantApp.BLL.Dtos.OrderDto;
using RestaurantApp.DAL.Models;

namespace RestaurantApp.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // MenuItem Mappings
            CreateMap<MenuItem, MenuItemReturnDto>();
            CreateMap<MenuItemCreateDto, MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MenuItemUpdateDto, MenuItem>();
                

            // Order Mappings
            CreateMap<Order, OrderReturnDto>();
            
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

            // OrderItem Mappings
            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(dest => dest.MenuItemId, opt => opt.MapFrom(src => src.MenuItemId))
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem!.Name))
                .ForMember(dest => dest.MenuItemPrice, opt => opt.MapFrom(src => src.MenuItem!.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.MenuItem!.Price * src.Count));
        }
    }
}

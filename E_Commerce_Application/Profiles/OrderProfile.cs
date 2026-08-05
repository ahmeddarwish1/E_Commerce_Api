using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Dtos.Orders;
using E_Commerce_Domain.Entities.Orders;

namespace E_Commerce_Application.Profiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
            .ForMember(dest => dest.DeliveryCost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}

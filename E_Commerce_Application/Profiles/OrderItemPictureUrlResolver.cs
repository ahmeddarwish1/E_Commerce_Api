using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using E_Commerce_Application.Dtos.Orders;
using E_Commerce_Domain.Entities.Orders;
using Microsoft.Extensions.Options;

namespace E_Commerce_Application.Profiles
{
    public class OrderItemPictureUrlResolver(IOptions<UrlSetting> options) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly UrlSetting settings=options.Value;
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            return $"{settings.BaseUrl}/Files/{source.Product.PictureUrl}";
        }
    }
}


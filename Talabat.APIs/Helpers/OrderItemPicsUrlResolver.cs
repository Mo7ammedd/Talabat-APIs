using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.APIs.Helpers;

public class OrderItemPicsUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration;

    public OrderItemPicsUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            return $"{_configuration["ApiBaseUrl"]}{source.Product.PictureUrl}";
        return null;
    }
}
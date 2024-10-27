using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTOs;
using Talabat.Core.Models;

namespace Talabat.APIs.Helpers;

public class MappingProfiles : Profile
{

    public MappingProfiles( )
    {
        CreateMap<Core.Models.Product, DTOs.ProductToReturnDto>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
            // .ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{_config["ApiBaseUrl"]}/{s.PictureUrl}"));
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();

    }
}
using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Models;

namespace Talabat.APIs.Helpers;

public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductPictureUrlResolver(IConfiguration configuration)
    {
        _configuration=configuration;
    }
    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
            return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
        return null;
    }
}
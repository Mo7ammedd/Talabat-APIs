using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.APIs.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices( this IServiceCollection services)
    {
        services.AddScoped(typeof(IOrderService), typeof(OrderService));
        services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
        // services.AddScoped<IBasketRepository, BasketRepository>();  
        services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
        // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(m => m.AddProfile<MappingProfiles>());
        services.AddScoped<ProductPictureUrlResolver>();
        
        services.Configure<ApiBehaviorOptions>(options => 
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = errors
                });
            });
        return services;
    }
}
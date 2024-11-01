using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIs.Extensions;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAuthService), typeof(AuthService));
        services.AddIdentity<AppUser,IdentityRole>(options =>
        {
            // options.Password.RequireNonAlphanumeric = false;
            // options.Password.RequireDigit = false;
            // options.Password.RequireLowercase = false;
            // options.Password.RequireUppercase = false;
            // options.Password.RequiredLength = 6;
        }).AddEntityFrameworkStores<AppIdentityDbContext>();
        return services;
    }
} 
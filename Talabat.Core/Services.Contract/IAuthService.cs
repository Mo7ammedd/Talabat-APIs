using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;

namespace Talabat.Core.Services.Contract;

public interface IAuthService
{
    
     Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager);

}
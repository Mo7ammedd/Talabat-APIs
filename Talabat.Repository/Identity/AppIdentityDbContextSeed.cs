using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "mohammed",
                Email = "mo@onvo.me",   
                UserName = "mohammed",
                PhoneNumber = "01112961724"
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }

}
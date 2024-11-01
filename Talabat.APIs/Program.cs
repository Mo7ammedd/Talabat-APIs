using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Controllers;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices();
            
            #endregion

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
          
            #region DatabaseMigration
            // Ask CLR to create a scope for the service provider
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
                await _identityDbContext.Database.MigrateAsync();
                var _userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManager); 
                
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "An error occurred during migration");
            }
            #endregion

            #region ConfigureMiddleware
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              app.UseSwaggerMiddleWare();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();
            #endregion

             app.Run();
        }
    }
}
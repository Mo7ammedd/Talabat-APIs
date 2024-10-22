using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Controllers;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(m => m.AddProfile<MappingProfiles>());
            builder.Services.AddScoped<ProductPictureUrlResolver>();
            builder.Services.Configure<ApiBehaviorOptions>(options => 
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
            var app = builder.Build();
            using var scope = app.Services.CreateScope();
          
                //ask clr to create a scope for the service provider
                var services = scope.ServiceProvider;
                var _dbcontext = services.GetRequiredService<StoreContext>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    await _dbcontext.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(_dbcontext);
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "An error occurred during migration");
                }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

 
            app.MapControllers();

            app.Run();
        }
    }
}

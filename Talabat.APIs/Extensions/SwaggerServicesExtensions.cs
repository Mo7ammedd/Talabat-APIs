using Microsoft.OpenApi.Models;

namespace Talabat.APIs.Extensions;

public static class  SwaggerServicesExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Version = "v1.0.0",
                Title = "Talabat APIs",
                Description = $"this is the API documentation for Talabat",
                Contact = new()
                {
                    Name = "Mohamed Taylor",
                    Email = "taylor@manga.com",
                }
            });

            c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Name = "Basic Authentication",
                Description = "Authorization header applied to request with Basic Schema.",
                In = ParameterLocation.Header,
                Scheme = "Basic",
                Type = SecuritySchemeType.Http
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }
    public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
    {
        app.UseSwagger(options => options.RouteTemplate = "/openapi/{documentName}.json");
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/openapi/v1.json", "My v1");
            x.EnablePersistAuthorization();
        });
        return app;
    }
}
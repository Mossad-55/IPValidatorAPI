using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi;
using System.Threading.RateLimiting;

namespace IPValidator_API.Extensions;

public static class ServiceExtension
{
    // IRepositoryManager Configuration
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddSingleton<IRepositoryManager, RepositoryManager>();

    // IServiceManager Configuration
    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddSingleton<IServiceManager, ServiceManager>();

    // Rate Limiting Configuration
    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(optionos =>
        {
            optionos.AddFixedWindowLimiter("GlobalLimiter", limiter =>
            {
                limiter.PermitLimit = 100;
                limiter.Window = TimeSpan.FromMinutes(1);
                limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                limiter.QueueLimit = 10;
            });
        });
    }

    // Coonfigure Swagger
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "IP Validator API",
                Version = "v1",
                Description = "API for validating IP addresses and retrieving country info"
            });
        });
    }
}

using Application.Interfaces;
using Application.Services;
using Domain.Configuration;
using Infrastructure.ExternalServices;
using Infrastructure.HostedServices;
using IPValidator_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRateLimiting();
builder.Services.Configure<IpGeolocationSettings>(
    builder.Configuration.GetSection("IpGeolocation"));

//builder.Services.AddHttpClient<IIpService, IpApiService>();
builder.Services.AddHttpClient<IIpService, IpGeolocationService>();

builder.Services.AddHttpClient<IpGeolocationService>();
builder.Services.AddHostedService<TemporalCleanupService>();
builder.Services.AddScoped<IpApplicationService>();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<Presentation.ActionFilters.ApiExceptionFitler>();
})
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "IP Validator API v1");
        options.RoutePrefix = string.Empty; // Opens Swagger at root URL
    });
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.MapControllers();

app.Run();
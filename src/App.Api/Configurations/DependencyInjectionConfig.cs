using App.Data.Context;
using App.Data.Repositories;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using App.Domain.Notifications;
using App.Domain.Notifications.Interfaces;
using App.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductService, ProductService>();
            
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}

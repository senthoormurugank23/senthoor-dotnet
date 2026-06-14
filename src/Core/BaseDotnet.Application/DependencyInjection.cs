using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using BaseDotnet.Application.Interfaces;
using BaseDotnet.Application.Services;

namespace BaseDotnet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}

using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOut.Application
{
    public static class ApplicationServiceRegistrations
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
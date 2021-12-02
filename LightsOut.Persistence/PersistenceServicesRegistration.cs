using LightsOut.Application.Persistence;
using LightsOut.Persistence.Context;
using LightsOut.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOut.Persistence
{
    public static class PersistenceServicesRegistration 
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LightsOutContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LightsOutConnectionString"));
            });
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBoardSettingRepository, BoardSettingsRepository>();
            services.AddScoped<IInitialStateRepository, InitialStateRepository>();

            return services;
        }
    }
}
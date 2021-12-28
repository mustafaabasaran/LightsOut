using System;
using System.Linq;
using System.Net.Http;
using LightsOut.Api.IntegrationTests.Helpers;
using LightsOut.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LightsOut.Api.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient TestClient;

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<LightsOutContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<LightsOutContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                        
                        services.Remove(services.SingleOrDefault(x=> x.ServiceType == typeof(IDistributedCache)));
                        services.AddSingleton<IDistributedCache, DistributedCacheMock>();
                        
                        var sp = services.BuildServiceProvider();
                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<LightsOutContext>();
                            var logger = scopedServices
                                .GetRequiredService<ILogger<IntegrationTests>>();

                            db.Database.EnsureCreated();

                            try
                            {
                                Utilities.InitializeDbForTests(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred seeding the " +
                                                    "database with test messages. Error: {Message}", ex.Message);
                            }
                        }
                    });
                });
            TestClient = appFactory.CreateClient();
        }
    }
}
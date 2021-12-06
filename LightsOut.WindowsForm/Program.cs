using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut.WindowsForm
{
    internal static class Program
    {
        public static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);

            Configuration = builder.Build();

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Board>();
                Application.Run(form1);
            }
        }

        private static void ConfigureServices(IServiceCollection services) 
        {
            services.AddScoped<Board>();
            services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri(Configuration["ApiUrl"]);
            });
        }
    }
}

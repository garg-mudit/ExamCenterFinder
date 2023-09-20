using System.Reflection;

using ExamCenterFinder.API.Data.Context;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamCenterFinder.Tests
{
    public class Program
    {
        #region Fields + Properties 
        private static Lazy<IServiceProvider> _serviceProvider = new Lazy<IServiceProvider>(() => InitDependencyInjection());
        protected static IServiceProvider ServiceProvider => _serviceProvider.Value;
        protected static IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();
        protected static ApplicationDbContext DbContext => ServiceProvider.GetRequiredService<ApplicationDbContext>();
        #endregion Fields + Properties

        // Private Methods
        private static IServiceProvider InitDependencyInjection()
        {
            var services = new ServiceCollection();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Ask the service provider for the configuration abstraction.
            var config = GetConfiguration();

            // Configure the DbContext with the connection string
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var cs = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(cs);
            });

            return services.BuildServiceProvider();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path for configuration
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Load the test configuration file

            return configBuilder.Build();
        }
    }
}

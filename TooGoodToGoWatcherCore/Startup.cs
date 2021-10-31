using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.Handlers;
using TooGoodToGoWatcherCore.Models;

namespace TooGoodToGoWatcherCore
{
    public static class Startup
    {
        public static IHostBuilder ConfigureServices()
        {
            IHostBuilder hostBuilder = new HostBuilder()

                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                    configBuilder.SetBasePath(hostContext.HostingEnvironment.ContentRootPath);
                    configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                })

                .ConfigureLogging((hostContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
#if DEBUG
                    loggingBuilder.AddConsole();
#endif
                })
                
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(hostContext.Configuration, services);

                    services.AddHostedService<CoreWatcher>();
                });

            return hostBuilder;
        }

        public static void ConfigureServices(IConfiguration configuration, IServiceCollection serviceCollection)
        {
            ////Load Configuration
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            //Load logging
            serviceCollection.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

            //Load Handlers
            serviceCollection.AddTransient<IApiHandler, ApiHandler>();
            serviceCollection.AddTransient<IConfigurationHandler, ConfigurationHandler>();
            serviceCollection.AddTransient<ITooGoodToGoSessionLoaderHandler, TooGoodToGoSessionLoaderHandler>();

            serviceCollection.AddTransient<ISoundHandler, SoundHandler>();
            serviceCollection.AddTransient<IIftttNotifier, IftttNotifier>();

            //Load CoreWatcher
            serviceCollection.AddSingleton<ICoreWatcher, CoreWatcher>();

            //Load Secrets (in singleton)
            serviceCollection.AddSingleton<Secrets>(LoadSecrets());
            serviceCollection.AddSingleton<Settings>(LoadSettings(configuration));
        }

        private static Secrets LoadSecrets()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            var secretProvider = config.Providers.First();
            if (!secretProvider.TryGet("Email", out var mail)) throw new NullReferenceException("Email cannot be null");
            if (!secretProvider.TryGet("Password", out var password)) throw new NullReferenceException("Password cannot be null");
            if (!secretProvider.TryGet("IftttEventName", out var iftttEventName)) throw new NullReferenceException("IftttEventName cannot be null");
            if (!secretProvider.TryGet("IftttKey", out var iftttKey)) throw new NullReferenceException("IftttKey cannot be null");

            return new Secrets()
            {
                Email = mail,
                Password = password,
                IftttEventName = iftttEventName,
                IftttKey = iftttKey
            };
        }

        private static Settings LoadSettings(IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection("Settings");
            Settings settings = BindConfigSection<Settings>(configurationSection);

            return settings;
        }

        private static T BindConfigSection<T>(IConfigurationSection configSection) where T : new ()
        {
            var binding = new T();
            configSection.Bind(binding);
            return binding;
        }
    }
}

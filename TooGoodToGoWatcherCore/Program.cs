using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System.IO;
using TooGoodToGoWatcherCore.Models;
using Microsoft.Extensions.Hosting;

namespace TooGoodToGoWatcherCore
{
    class Program
    {
        public static ILoggerFactory LogFactory { get; } = LoggerFactory.Create(builder =>
        {
            builder.ClearProviders();
            // Clear Microsoft's default providers (like eventlogs and others)
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            }).SetMinimumLevel(LogLevel.Warning);
        });

        public static ILogger<T> CreateLogger<T>() => LogFactory.CreateLogger<T>();

        static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = Startup.ConfigureServices();
            await hostBuilder.RunConsoleAsync();

            //ILogger logger = CreateLogger<Program>();

            //// Create service collection
            //logger.LogInformation("Creating service collection");
            //ServiceCollection serviceCollection = new ServiceCollection();
            //await ConfigureServices(serviceCollection);

            //// Create service provider
            //logger.LogInformation("Building service provider");
            //IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ////Secrets secrets = new Secrets();
            ////var config = serviceProvider.GetService<IConfiguration>();
            ////config.Bind(secrets);

            //// Create service provider
            //logger.LogInformation("Starting TooGoodToGoWatcherCore");
            //ICoreWatcher coreWatcher = serviceProvider.GetService<ICoreWatcher>();
            //await coreWatcher.Run();
        }
    }
}

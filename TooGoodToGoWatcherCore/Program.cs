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
        static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = Startup.ConfigureServices();
            await hostBuilder.RunConsoleAsync();
        }
    }
}

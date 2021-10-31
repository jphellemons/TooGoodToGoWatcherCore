using System.Threading.Tasks;
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

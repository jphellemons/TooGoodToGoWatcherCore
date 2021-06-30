using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TooGoodToGoWatcherCore.DataContracts;
using TooGoodToGoWatcherCore.Handlers;

namespace TooGoodToGoWatcherCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool continuesRunning = true;
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            
            var secretProvider = config.Providers.First();
            if (!secretProvider.TryGet("Email", out var mail) ) return;
            if (!secretProvider.TryGet("Password", out var password) ) return;

            do
            {
                if (!continuesRunning)
                {
                    Console.WriteLine();
                }

                using (ApiHandler apiHandler = new ApiHandler(mail, password))
                {
                    var loginResponse = await apiHandler.GetSession();
                    var lfResponse = await apiHandler.GetFavoriteList(loginResponse);

                    if (lfResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var debugResponse = await lfResponse.Content.ReadAsStringAsync();
                        var favResponse = await lfResponse.Content.ReadFromJsonAsync<FavoriteResponse>();

                        foreach (var item in favResponse.Items.Where(i => i.InSalesWindow && i.ItemsAvailable > 0))
                        {
                            double price = item.Item.Price.MinorUnits;
                            for (int i = 0; i < item.Item.Price.Decimals; i++)
                            {
                                price = price / 10;
                            }
                            Console.WriteLine($"{item.DisplayName} has {item.ItemsAvailable} for {price} {item.Item.Price.Code}. Pickup time: {item.PickupInterval.Start.ToString("dd-MM-yyyy HH:mm")} - {item.PickupInterval.End.ToString("dd-MM-yyyy HH:mm")}");
                            Console.Beep();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Loading Favorite failed with code {lfResponse.StatusCode}");
                    }
                }

                if (continuesRunning)
                {
                    Console.WriteLine("Sleep for 30 seconds");
                    await Task.Delay(30000);
                }
                else
                {
                    Console.WriteLine("Waiting for next round. Press Q to continue");
                }
            }
            while (continuesRunning || Console.ReadKey().Key.Equals(ConsoleKey.Q));
        }
    }
}

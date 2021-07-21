using System;
using System.Collections.Generic;
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
            if (!secretProvider.TryGet("IftttEventName", out var iftttEventName) ) return;
            if (!secretProvider.TryGet("IftttKey", out var iftttKey) ) return;

            var iNotifier = new IftttNotifier(iftttEventName, iftttKey);
            var previousFavResponse = new List<ItemElement>();
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

                        var availableFav = favResponse.Items.Where(i => i.InSalesWindow && i.ItemsAvailable > 0).ToList();
                        if (!previousFavResponse.SequenceEqual(availableFav))
                        {
                            foreach (var item in availableFav)
                            {
                                double price = item.Item.Price.MinorUnits;
                                for (int i = 0; i < item.Item.Price.Decimals; i++)
                                {
                                    price = price / 10;
                                }
                                Console.WriteLine($"{item.DisplayName} has {item.ItemsAvailable} for {price.ToString("C")} {item.Item.Price.Code}. Pickup time: {item.PickupInterval.Start.ToLocalTime()} - {item.PickupInterval.End.ToLocalTime()}");
                                Console.Beep();

                                iNotifier.Notify(item);
                            }
                            previousFavResponse = availableFav;
                        }
                    }
                    else
                    {
                        if (lfResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            apiHandler.ResetSession();
                        }
                        else
                        {
                            Console.WriteLine($"Loading Favorite failed with code {lfResponse.StatusCode}");
                        }
                    }
                }

                if (continuesRunning)
                {
                    Console.WriteLine("Sleep for 60 seconds");
                    await Task.Delay(60000);
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

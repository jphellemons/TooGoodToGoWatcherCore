using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TooGoodToGoWatcherCore.DataContracts;
using TooGoodToGoWatcherCore.Handlers;
using TooGoodToGoWatcherCore.Models;

namespace TooGoodToGoWatcherCore
{
    public class CoreWatcher : ICoreWatcher, IHostedService
    {
        private Task executingTask;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private IApiHandler apiHandler;
        private IIftttNotifier iftttNotifier;
        private Settings settings;

        public CoreWatcher(IApiHandler apiHandler, IIftttNotifier iftttNotifier, Settings settings)
        {
            this.apiHandler = apiHandler;
            this.iftttNotifier = iftttNotifier;
            this.settings = settings;
        }

        public async Task Run()
        {
            SoundPlayer player = new SoundPlayer(settings.SoundFile);

            var previousFavResponse = new List<ItemElement>();
            do
            {
                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    Console.WriteLine();
                }

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

                            if (settings.ConsoleBeepOnNotification)
                            {
                                Console.Beep();
                            }

                            if (settings.SoundOnNotification)
                            {
                                player.Play();
                            }

                            iftttNotifier.Notify(item);
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

                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    Console.WriteLine("Sleep for 60 seconds");
                    for (int i = 0; !cancellationTokenSource.IsCancellationRequested && i < 60; i++)
                    {
                        await Task.Delay(1000);
                    }
                }
            }
            while (!cancellationTokenSource.IsCancellationRequested);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            executingTask = Run();

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Signal cancellation to the executing method
                cancellationTokenSource.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }
    }
}

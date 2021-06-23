using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using TooGoodToGoWatcherCore.DataContracts;
using TooGoodToGoWatcherCore.Handlers;

namespace TooGoodToGoWatcherCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            do {
                Console.WriteLine();

                using (ApiHandler apiHandler = new ApiHandler())
                {
                    var loginResponse = await apiHandler.GetSession();
                    var lfResponse = await apiHandler.GetFavoriteList(loginResponse);

                    if (lfResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var debugResponse = await lfResponse.Content.ReadAsStringAsync();
                        var favResponse = await lfResponse.Content.ReadFromJsonAsync<FavoriteResponse>();

                        foreach (var item in favResponse.Items.Where(i => i.InSalesWindow && i.ItemsAvailable > 0))
                        {
                            System.Console.WriteLine($"{item.DisplayName}, {item.Item.Price}");
                            /*new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                .AddText(item.DisplayName)
                                .AddText("Check this out, The Enchantments in Washington!")
                                .Show();*/
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Loading Favorite failed with code {lfResponse.StatusCode}");
                    }
                }

/*var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    },
    PropertyNameCaseInsensitive = true
};*/



                

                //Console.Read();
                Console.WriteLine("Waiting for next round. Press Q to continue");
            } while (Console.ReadKey().Key.Equals(ConsoleKey.Q));
        }

    }
}

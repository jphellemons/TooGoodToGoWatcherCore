using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;

namespace TooGoodToGoWatcherCore
{
    class Program
    {
        private const string tgtgApi = "https://apptoogoodtogo.com/api/";
        
        static async Task Main(string[] args)
        {
            do {
                Console.WriteLine();

                using var hc = new HttpClient();
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                hc.DefaultRequestHeaders.Add("Accept-Language", "en-US");

                var loginResponse = await GetSession(hc);

                hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.AccessToken);

/*var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    },
    PropertyNameCaseInsensitive = true
};*/

                var lfResponse = await hc.PostAsJsonAsync(tgtgApi + "item/v7/", GetListFavorites(loginResponse.StartupData.User.UserId));

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

                //Console.Read();
                Console.WriteLine("Waiting for next round. Press Q to continue");
            } while (Console.ReadKey().Key.Equals(ConsoleKey.Q));
        }

        private static async Task<LoginResponse> GetSession(HttpClient httpClient)
        {
            TooGoodToGoSessionLoader tooGoodToGoSessionLoader = new TooGoodToGoSessionLoader();
            var loginResponse = await tooGoodToGoSessionLoader.Load();

            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.RefreshToken))
            {
                var response = await httpClient.PostAsJsonAsync(tgtgApi + "auth/v1/loginByEmail", GetLoginRequest());

                loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                await tooGoodToGoSessionLoader.Save(loginResponse);
            }

            return loginResponse;

        }

        private static ListFavorites GetListFavorites(long userId)
        {
            return new ListFavorites()
            {
                FavoriteOnly = true,
                Origin = new Coordinates() { Latitude = 51.697815, Longitude = 5.303675},
                Radius = 15,
                UserId = userId
            };
        }

        private static LoginRequest GetLoginRequest()
        { 
            return new LoginRequest()
            {
                Email = "info@jphellemons.nl",
                Password = "Your-password-here",
                DeviceType = "UNKNOWN"
            };
        }
    }
}

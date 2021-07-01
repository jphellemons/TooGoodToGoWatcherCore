using System;
using System.Net.Http;
using System.Net.Http.Json;
using TooGoodToGoWatcherCore.DataContracts;

namespace TooGoodToGoWatcherCore
{
    public class IftttNotifier
    {
        private string iftttEventName;
        private string iftttKey;

        public IftttNotifier(string iftttEventName, string iftttKey)
        {
            this.iftttEventName = iftttEventName;
            this.iftttKey = iftttKey;
        }

        public async void Notify(ItemElement item)
        {
            try
            {
                string iftttUrl = $"https://maker.ifttt.com/trigger/{iftttEventName}/with/key/{iftttKey}";

                double price = item.Item.Price.MinorUnits;
                for (int i = 0; i < item.Item.Price.Decimals; i++)
                {
                    price = price / 10;
                }

                var payload = new IftttPayload(){ 
                    value1= item.DisplayName, 
                    value2 = $"{price} {item.Item.Price.Code}", 
                    value3 = $"Pickup time: {item.PickupInterval.Start.ToString("dd-MM-yyyy HH:mm")} - {item.PickupInterval.End.ToString("dd-MM-yyyy HH:mm")}"
                    };
                var _httpClient = new HttpClient();
                var result = await _httpClient.PostAsJsonAsync(iftttUrl, payload);
                System.Console.WriteLine(result.StatusCode);
                System.Console.WriteLine(await result.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
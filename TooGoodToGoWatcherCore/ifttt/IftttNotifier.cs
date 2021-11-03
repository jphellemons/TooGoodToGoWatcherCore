using System;
using System.Net.Http;
using System.Net.Http.Json;
using TooGoodToGoWatcherCore.DataContracts;
using TooGoodToGoWatcherCore.Models;

namespace TooGoodToGoWatcherCore
{
    public class IftttNotifier : IIftttNotifier
    {
        private string iftttEventName;
        private string iftttKey;

        public IftttNotifier(Secrets secrets)
        {
            this.iftttEventName = secrets.IftttEventName;
            this.iftttKey = secrets.IftttKey;
        }

        public async void Notify(ItemElement item)
        {
            try
            {
                if(string.IsNullOrEmpty(iftttEventName) || string.IsNullOrEmpty(iftttKey))
                {
                    return;
                }
                string iftttUrl = $"https://maker.ifttt.com/trigger/{iftttEventName}/with/key/{iftttKey}";

                double price = item.Item.PriceIncludingTaxes.MinorUnits;
                for (int i = 0; i < item.Item.PriceIncludingTaxes.Decimals; i++)
                {
                    price = price / 10;
                }

                var payload = new IftttPayload(){ 
                    value1= item.DisplayName, 
                    value2 = $"{price.ToString("C")} {item.Item.PriceIncludingTaxes.Code}", 
                    value3 = $"Pickup time: {item.PickupInterval.Start.ToLocalTime()} - {item.PickupInterval.End.ToLocalTime()}"
                    };
                var _httpClient = new HttpClient();
                var result = await _httpClient.PostAsJsonAsync(iftttUrl, payload);
                
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    System.Console.WriteLine(result.StatusCode);
                    System.Console.WriteLine(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
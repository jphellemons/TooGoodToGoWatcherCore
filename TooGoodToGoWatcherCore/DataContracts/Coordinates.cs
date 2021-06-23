using System.Text.Json.Serialization;

namespace TooGoodToGoWatcherCore.DataContracts
{
    public class Coordinates
    {
        [JsonPropertyNameAttribute("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyNameAttribute("longitude")]
        public double Longitude { get; set; }

    }
}
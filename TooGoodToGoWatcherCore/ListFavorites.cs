using System.Text.Json.Serialization;

namespace TooGoodToGoWatcherCore
{
    public class ListFavorites
    {
        [JsonPropertyNameAttribute("favorites_only")]
        public bool FavoriteOnly { get; set; }

        [JsonPropertyNameAttribute("origin")]
        public Coordinates Origin { get; set; }

        [JsonPropertyNameAttribute("radius")]
        public int Radius { get; set; }

        [JsonPropertyNameAttribute("user_id")]
        public long UserId { get; set; }
    }
}

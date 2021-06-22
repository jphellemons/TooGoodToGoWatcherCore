using System.Text.Json.Serialization;

namespace TooGoodToGoWatcherCore
{
    public class LoginRequest
    {
        [JsonPropertyNameAttribute("device_type")]
        public string DeviceType { get; internal set; }

        public string Email { get; internal set; }
        public string Password { get; internal set; }
    }
}
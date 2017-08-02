using Newtonsoft.Json;

namespace WeatherClient.Models
{
    [JsonObject("coord")]
    public class Coordinates
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
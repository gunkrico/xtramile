using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class SysDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("subrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }
}

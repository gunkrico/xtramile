using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class CloudDto
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}

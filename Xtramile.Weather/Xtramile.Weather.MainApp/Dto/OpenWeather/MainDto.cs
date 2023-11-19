using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class MainDto
    {
        [JsonProperty("temp")]
        public decimal Temperature { get; set; }

        [JsonProperty("feels_like")]
        public decimal FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public decimal TemperatureMin { get; set; }

        [JsonProperty("temp_max")]
        public decimal TemperatureMax { get; set; }

        [JsonProperty("pressure")]
        public int Preasure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}

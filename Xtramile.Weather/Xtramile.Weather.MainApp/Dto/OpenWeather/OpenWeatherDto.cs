using System.Collections.Generic;
using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class OpenWeatherDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public CoordinateDto Coordinate { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDto> Weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainDto Main { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public WindDto Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudDto Clouds { get; set; }

        [JsonProperty("dt")]
        public int Dt { get; set; }

        [JsonProperty("timezone")]
        public int TimeZone { get; set; }

        [JsonProperty("sys")]
        public SysDto Sys { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}

using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class CoordinateDto
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}

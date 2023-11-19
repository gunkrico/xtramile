using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Dto.OpenWeather
{
    public class ApiErrorDto
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

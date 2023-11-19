using Newtonsoft.Json;

namespace Xtramile.Weather.MainApp.Model.Response
{
    public class ApiErrorResponseModel
    {
        [JsonProperty("errorMessages")]
        public string[] ErrorMessages { get; set; }
    }
}

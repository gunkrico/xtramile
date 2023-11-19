using Newtonsoft.Json;
using System;

namespace Xtramile.Weather.MainApp.ServiceContract.Response
{
    public class ErrorApiResponse
    {
        [JsonProperty(PropertyName = "errorMessages")]

        public string[] ErrorMessages { get; set; } = Array.Empty<string>();
    }
}

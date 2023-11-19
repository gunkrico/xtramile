using Newtonsoft.Json;
using System.Threading.Tasks;
using Xtramile.Weather.MainApp.Core.Constant;
using Xtramile.Weather.MainApp.Dto.Weather;
using Xtramile.Weather.MainApp.ServiceContract;
using Xtramile.Weather.MainApp.ServiceContract.Response;
using Xtramile.Weather.MainApp.ServiceContract.Weather;

namespace Xtramile.Weather.MainApp.Service.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly IApiService apiService;

        public WeatherService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public GenericResponse<WeatherDto> GetByCityName(string cityName)
        {
            var response = new GenericResponse<WeatherDto>();
            var url = string.Format(OpenWeatherApiConstant.Url, cityName, OpenWeatherApiConstant.Key);
            var weatherApiResponse = Task.Run(() => apiService.SendRequestAsync(url, "GET")).GetAwaiter().GetResult();

            if (weatherApiResponse.IsError())
            {
                response.AddErrorMessage(weatherApiResponse.GetErrorMessage());
                return response;
            }

            var result = JsonConvert.DeserializeObject<Dto.OpenWeather.OpenWeatherDto>(weatherApiResponse.Data);
            response.Data = GetWeather(result);
            return response;
        }

        private WeatherDto GetWeather(Dto.OpenWeather.OpenWeatherDto apiData)
        {
            var weatherDto = new WeatherDto()
            {
                Country = apiData.Sys.Country,
                City = apiData.Name,
                Time = apiData.TimeZone,
                DewPoint = 0,
                Humidity = apiData.Main.Humidity,
                Location = $"lon: {apiData.Coordinate.Longitude}, lat: {apiData.Coordinate.Latitude}",
                SkyCondition = apiData.Weather[0].Main,
                TemperatureFahrenheit = apiData.Main.Temperature,
                TemperatureCelcius = GetFahrenheitInCelcious(apiData.Main.Temperature),
                Visibility = apiData.Visibility,
                Wind = apiData.Wind.Speed,
                WindDirection = apiData.Wind.Deg,
                Pressure = apiData.Main.Preasure,
            };

            return weatherDto;
        }

        private decimal GetFahrenheitInCelcious(decimal fahrenheitTemperature)
        {
            return (fahrenheitTemperature - 32) * 5 / 9;
        }
    }
}

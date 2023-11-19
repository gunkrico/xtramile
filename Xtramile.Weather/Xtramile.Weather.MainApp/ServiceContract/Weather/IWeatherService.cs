using Xtramile.Weather.MainApp.Dto.Weather;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.ServiceContract.Weather
{
    public interface IWeatherService
    {
        GenericResponse<WeatherDto> GetByCityName(string cityName);
    }
}

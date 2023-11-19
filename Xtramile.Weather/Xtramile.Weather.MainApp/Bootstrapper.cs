using Microsoft.Extensions.DependencyInjection;
using Xtramile.Weather.MainApp.Repository.City;
using Xtramile.Weather.MainApp.Repository.Country;
using Xtramile.Weather.MainApp.RepositoryContract.City;
using Xtramile.Weather.MainApp.RepositoryContract.Country;
using Xtramile.Weather.MainApp.Service;
using Xtramile.Weather.MainApp.Service.City;
using Xtramile.Weather.MainApp.Service.Country;
using Xtramile.Weather.MainApp.Service.Weather;
using Xtramile.Weather.MainApp.ServiceContract;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ServiceContract.Weather;


namespace Xtramile.Weather.MainApp
{
    public class Bootstrapper
    {
        public static void SetupRepository(IServiceCollection service)
        {
            service.AddTransient<ICountryRepository, CountryRepository>();
            service.AddTransient<ICityRepository, CityRepository>();
        }

        public static void SetupServices(IServiceCollection service)
        {
            service.AddTransient<IApiService, ApiService>();
            service.AddTransient<ICountryService, CountryService>();
            service.AddTransient<ICityService, CityService>();
            service.AddTransient<IWeatherService, WeatherService>();
        }
    }
}

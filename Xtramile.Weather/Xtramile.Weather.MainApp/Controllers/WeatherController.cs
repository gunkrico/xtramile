using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Xtramile.Weather.MainApp.Dto.Weather;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Weather;
using Xtramile.Weather.MainApp.ViewModel.Weather;

namespace Xtramile.Weather.MainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : BaseController
    {
        #region Fields

        private readonly IWeatherService weatherService;
        private readonly ICityService cityService;

        #endregion

        #region Constructor

        public WeatherController(IWeatherService weatherService,
            ICityService cityService)
        {
            this.weatherService = weatherService;
            this.cityService = cityService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Get Weather based by City Name.</remarks>
        /// <param name="id">City id.</param>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Invalid Request.</response>
        /// <response code="401">Api Key not provided or incorrect.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/weather/city/{id}")]
        public IActionResult GetByCityId([FromRoute][Required] int id)
        {
            if (!IsValidRequest(id))
            {
                return GetApiError("Invalid Request", (int)HttpStatusCode.BadRequest);
            }

            var cityResponse = cityService.GetById(id);
            if (cityResponse.IsError())
            {
                return GetApiError(cityResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var weatherResponse = weatherService.GetByCityName(cityResponse.Data.Name);
            if (weatherResponse.IsError())
            {
                return GetApiError(weatherResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var response = DtoToViewModel(weatherResponse.Data);

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private bool IsValidRequest(int id)
        {
            return id > 0;
        }

        private WeatherVM DtoToViewModel(WeatherDto dto)
        {
            return new WeatherVM()
            {
                City = dto.City,
                Country = dto.Country,
                DewPoint = dto.DewPoint,
                Humidity = dto.Humidity,
                Location = dto.Location,
                Pressure = dto.Pressure,
                SkyCondition = dto.SkyCondition,
                TemperatureCelcius = string.Format("{0:0.00}", dto.TemperatureCelcius),
                TemperatureFahrenheit = string.Format("{0:0.00}", dto.TemperatureFahrenheit),
                Time = dto.Time,
                Visibility = dto.Visibility,
                Wind = dto.Wind,
                WindDirection = dto.WindDirection,
            };
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ViewModel.City;

namespace Xtramile.Weather.MainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        #region Fields

        private readonly ICityService cityService;
        private readonly ICountryService countryService;

        #endregion

        #region Constructor

        public CityController(ICityService cityService,
            ICountryService countryService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Get list city by country id.</remarks>
        /// <param name="id">Country id.</param>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Invalid Request.</response>
        /// <response code="401">Api Key not provided or incorrect.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/city/{id}/country")]
        public IActionResult GetByCountryId([FromRoute][Required] int id)
        {
            if (!IsValidRequest(id))
            {
                return GetApiError("Invalid Request", (int)HttpStatusCode.BadRequest);
            }

            var countryResponse = countryService.GetById(id);
            if (countryResponse.IsError())
            {
                return GetApiError(countryResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var cityResponse = cityService.GetByCountryId(id);
            if (cityResponse.IsError())
            {
                return GetApiError(cityResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var cityModels = cityResponse.DtoCollection.Select(c => DtoToViewModel(c)).ToList();

            var response = new CityListVM()
            {
                Results = cityModels,
            };

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private bool IsValidRequest(int id)
        {
            return id > 0;
        }

        private CityVM DtoToViewModel(CityDto dto)
        {
            return new CityVM()
            {
                Id = dto.Id,
                Name = dto.Name,
                CountryId = dto.CountryId,
            };
        }

        #endregion
    }
}

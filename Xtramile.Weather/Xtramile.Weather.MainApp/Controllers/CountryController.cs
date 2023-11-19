using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ViewModel.Country;

namespace Xtramile.Weather.MainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        #region Fields

        private readonly ICountryService countryService;

        #endregion

        #region Constructor

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Get List of countries.</remarks>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Invalid Request.</response>
        /// <response code="401">Api Key not provided or incorrect.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/country")]
        public IActionResult GetAll()
        {
            var countryResponse = countryService.GetAll();
            if (countryResponse.IsError())
            {
                return GetApiError(countryResponse.GetMessageErrorTextArray(), (int)HttpStatusCode.BadRequest);
            }

            var countryModels = countryResponse.DtoCollection.Select(c => DtoToViewModel(c)).ToList();

            var response = new CountryListVM()
            {
                Results = countryModels,
            };

            return new OkObjectResult(response);

        }

        #endregion

        #region Private Methods

        private CountryVM DtoToViewModel(CountryDto dto)
        {
            return new CountryVM()
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        #endregion
    }
}

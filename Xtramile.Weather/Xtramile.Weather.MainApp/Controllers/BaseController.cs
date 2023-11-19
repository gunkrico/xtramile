using Microsoft.AspNetCore.Mvc;
using Xtramile.Weather.MainApp.Model.Response;

namespace Xtramile.Weather.MainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult GetApiError(string[] errorMessages, int? httpStatusCode = null)
        {
            var actualStatusCode = httpStatusCode.HasValue ? httpStatusCode.Value : 400;
            var responseObj = new ApiErrorResponseModel
            {
                ErrorMessages = errorMessages,
            };

            return StatusCode(actualStatusCode, responseObj);
        }

        protected IActionResult GetApiError(string errorMessage, int? httpStatusCode = null)
        {
            var actualStatusCode = httpStatusCode.HasValue ? httpStatusCode.Value : 400;
            var responseObj = new ApiErrorResponseModel
            {
                ErrorMessages = new string[] { errorMessage },
            };

            return StatusCode(actualStatusCode, responseObj);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xtramile.Weather.MainApp.Controllers;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.Dto.Weather;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Response;
using Xtramile.Weather.MainApp.ServiceContract.Weather;
using Xunit;

namespace Xtramile.Weather.UnitTest.Controller.Weather
{
    public class WeatherControllerUnitTest
    {
        #region Fields

        private readonly Mock<IWeatherService> serviceMock;
        private readonly Mock<ICityService> cityServiceMock;
        private readonly WeatherController controller;

        #endregion

        #region Constructor

        public WeatherControllerUnitTest()
        {
            this.serviceMock = new Mock<IWeatherService>();
            this.cityServiceMock = new Mock<ICityService>();
            this.controller = new WeatherController(this.serviceMock.Object, this.cityServiceMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCityId

        [Fact]
        public void GetByCityId_ValidCity_ReturnCorrectWeather()
        {
            this.PreparationGetCityById(true);
            this.PreparationGetByCityName(true);

            var result = this.controller.GetByCityId(1);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCityName(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GetByCityId_EmptyCity_ReturnBadRequest()
        {
            this.PreparationGetCityById(false);
            this.PreparationGetByCityName(false);

            var result = this.controller.GetByCityId(0);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Never());
            this.serviceMock.Verify(c => c.GetByCityName(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetByCityId_InvalidCity_ReturnBadRequest()
        {
            this.PreparationGetCityById(false);
            this.PreparationGetByCityName(true);

            var result = this.controller.GetByCityId(9);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCityName(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void GetByCityId_GetWeatherErrorFound_ReturnBadRequest()
        {
            this.PreparationGetCityById(true);
            this.PreparationGetByCityName(false);

            var result = this.controller.GetByCityId(1);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.cityServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCityName(It.IsAny<string>()), Times.Once());
        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationGetCityById(bool isValid)
        {
            var response = new GenericResponse<CityDto>();

            if (isValid)
            {
                response.Data = new CityDto() { Id = 1, Name = "Jakarta", CountryId = 1 };
            }
            else
            {
                response.AddErrorMessage("City cannot be found.");
            }

            this.cityServiceMock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(response);
        }

        private void PreparationGetByCityName(bool isValid)
        {
            var response = new GenericResponse<WeatherDto>();

            if (isValid)
            {
                var weatherData = new WeatherDto() { Humidity = 25, SkyCondition = "rain" };
                response.Data = weatherData;
                this.serviceMock.Setup(item => item.GetByCityName(It.IsAny<string>()))
                    .Returns(response);
            }
            else
            {
                response.AddErrorMessage("Error message");
                this.serviceMock.Setup(item => item.GetByCityName(It.IsAny<string>()))
                    .Returns(response);
            }
        }

        #endregion
    }
}

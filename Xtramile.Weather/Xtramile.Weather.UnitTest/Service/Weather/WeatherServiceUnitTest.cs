using Moq;
using Xtramile.Weather.MainApp.Service.Weather;
using Xtramile.Weather.MainApp.ServiceContract;
using Xtramile.Weather.MainApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service.Weather
{
    public class WeatherServiceUnitTest
    {
        #region Fields

        private readonly Mock<IApiService> apiServiceMock;
        private readonly WeatherService service;

        #endregion

        #region Constructor

        public WeatherServiceUnitTest()
        {
            apiServiceMock = new Mock<IApiService>();
            service = new WeatherService(apiServiceMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCityName

        [Fact]
        public void GetByCityName_ValidCity_ReturnCorrectValue()
        {
            this.PreparationSendRequestAsync(true);

            var result = this.service.GetByCityName("Jakarta");

            Assert.NotNull(result.Data);
            Assert.Equal("ID", result.Data.Country);
            this.apiServiceMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void GetByCityName_ValidCity_ReturnCorrectTemperature()
        {
            decimal fahrenheitTemperature = 79.66M;
            decimal celciusTemperature = 26.477777777777777777777777778M;
            this.PreparationSendRequestAsync(true, fahrenheitTemperature);

            var result = this.service.GetByCityName("Jakarta");

            Assert.NotNull(result.Data);
            Assert.Equal(celciusTemperature, result.Data.TemperatureCelcius);
            this.apiServiceMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public void GetByCityName_InValidCity_ReturnErrorMessage()
        {
            this.PreparationSendRequestAsync(false);

            var result = this.service.GetByCityName("Gotham");

            Assert.Null(result.Data);
            Assert.Equal("city not found", result.GetErrorMessage());
            this.apiServiceMock.Verify(c => c.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationSendRequestAsync(bool isValid, decimal temperatureInFahrenheit = 32)
        {
            if (isValid)
            {
                var validResponse = new GenericResponse<string>()
                {
                    Data = "{\"coord\":{\"lon\":106.8451,\"lat\":-6.2146},\"weather\":[{\"id\":721,\"main\":\"Haze\",\"description\":\"haze\",\"icon\":\"50d\"}],\"base\":\"stations\",\"main\":{\"temp\":79.66,\"feels_like\":79.66,\"temp_min\":77.09,\"temp_max\":86.52,\"pressure\":1010,\"humidity\":88},\"visibility\":5000,\"wind\":{\"speed\":3.44,\"deg\":210},\"clouds\":{\"all\":20},\"dt\":1700350781,\"sys\":{\"type\":1,\"id\":9383,\"country\":\"ID\",\"sunrise\":1700346342,\"sunset\":1700391018},\"timezone\":25200,\"id\":1642911,\"name\":\"Jakarta\",\"cod\":200}"
                };

                apiServiceMock.Setup(a => a.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(validResponse);
            }
            else
            {
                var responseWithError = new GenericResponse<string>();
                responseWithError.AddErrorMessage("city not found");

                apiServiceMock.Setup(a => a.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(responseWithError);
            }
        }

        #endregion
    }
}

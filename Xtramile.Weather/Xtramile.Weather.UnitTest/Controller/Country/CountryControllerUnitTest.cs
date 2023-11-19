using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xtramile.Weather.MainApp.Controllers;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Controller.Country
{
    public class CityControllerUnitTest
    {
        #region Fields

        private readonly Mock<ICountryService> serviceMock;
        private readonly CountryController controller;

        #endregion

        #region Constructor

        public CityControllerUnitTest()
        {
            this.serviceMock = new Mock<ICountryService>();
            this.controller = new CountryController(this.serviceMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetAll

        [Fact]
        public void GetAll_Valid_ReturnListOfCountry()
        {
            this.PreparationGetAll(true);

            var result = this.controller.GetAll();
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.serviceMock.Verify(c => c.GetAll(), Times.Once());
        }

        [Fact]
        public void GetAll_Invalid_ReturnError()
        {
            this.PreparationGetAll(false);

            var result = this.controller.GetAll();
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.serviceMock.Verify(c => c.GetAll(), Times.Once());
        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationGetAll(bool isValid)
        {
            var response = new GenericGetDtoCollectionResponse<CountryDto>();

            if (isValid)
            {
                var countries = new List<CountryDto>
                {
                    new CountryDto() { Id = 1, Name = "Indonesia" },
                    new CountryDto() { Id = 2, Name = "Australia"},
                    new CountryDto() { Id = 3, Name = "UK" }
                };

                foreach (var country in countries)
                {
                    response.DtoCollection.Add(country);
                }    
            }
            else
            {
                response.AddErrorMessage("Country cannot be found.");
            }

            this.serviceMock.Setup(c => c.GetAll())
                    .Returns(response);
        }

        #endregion
    }
}

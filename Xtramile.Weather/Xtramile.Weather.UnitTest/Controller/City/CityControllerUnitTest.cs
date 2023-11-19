using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xtramile.Weather.MainApp.Controllers;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ServiceContract.Response;
using Xunit;

namespace Xtramile.Weather.UnitTest.Controller.City
{
    public class CityControllerUnitTest
    {
        #region Fields

        private readonly Mock<ICityService> serviceMock;
        private readonly Mock<ICountryService> countryServiceMock;
        private readonly CityController controller;

        #endregion

        #region Constructor

        public CityControllerUnitTest()
        {
            this.serviceMock = new Mock<ICityService>();
            this.countryServiceMock = new Mock<ICountryService>();
            this.controller = new CityController(this.serviceMock.Object, this.countryServiceMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCountryId

        [Fact]
        public void GetByCountryId_ValidCountry_ReturnCorrectValue()
        {
            this.PreparationGetCountryById(true);
            this.PreparationGetByCountryId(true);

            var result = this.controller.GetByCountryId(1);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void GetByCountryId_EmptyCountry_ReturnBadRequest()
        {
            this.PreparationGetCountryById(false);
            this.PreparationGetByCountryId(false);

            var result = this.controller.GetByCountryId(0);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Never());
            this.serviceMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void GetByCountryId_InvalidCountry_ReturnBadRequest()
        {
            this.PreparationGetCountryById(false);
            this.PreparationGetByCountryId(true);

            var result = this.controller.GetByCountryId(9);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void GetAllByCountryId_ErrorFound_ReturnBadRequest()
        {
            this.PreparationGetCountryById(true);
            this.PreparationGetByCountryId(false);

            var result = this.controller.GetByCountryId(1);
            var actualResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status400BadRequest, actualResult.StatusCode);
            this.countryServiceMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
            this.serviceMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Once());
        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationGetCountryById(bool isValid)
        {
            var response = new GenericResponse<CountryDto>();

            if (isValid)
            {
                response.Data = new CountryDto() { Id = 1, Name = "Indonesia" };
            }
            else
            {
                response.AddErrorMessage("Country with id 9 cannot be found.");
            }

            this.countryServiceMock.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(response);
        }

        private void PreparationGetByCountryId(bool isValid)
        {
            var response = new GenericGetDtoCollectionResponse<CityDto>();

            if (isValid)
            {
                var cities = new List<CityDto>
                {
                    //Indonesia
                    new CityDto() { Id = 1, Name = "Jakarta", CountryId = 1 },
                    new CityDto() { Id = 2, Name = "Denpasar", CountryId = 1 },
                    new CityDto() { Id = 3, Name = "Bandung", CountryId = 1 },
                    //Australia
                    new CityDto() { Id = 4, Name = "Sydney", CountryId = 2 },
                    new CityDto() { Id = 5, Name = "Melbourne", CountryId = 2 },
                    new CityDto() { Id = 6, Name = "Perth", CountryId = 2 },
                    //UK
                    new CityDto() { Id = 7, Name = "London", CountryId = 3 },
                    new CityDto() { Id = 8, Name = "Liverpool", CountryId = 3 },
                    new CityDto() { Id = 9, Name = "Manchester", CountryId = 3 }
                };

                foreach (var city in cities)
                {
                    response.DtoCollection.Add(city);
                }    
            }
            else
            {
                response.AddErrorMessage("City cannot be found.");
            }

            this.serviceMock.Setup(c => c.GetByCountryId(It.IsAny<int>()))
                    .Returns(response);
        }

        #endregion
    }
}

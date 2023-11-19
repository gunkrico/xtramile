using Moq;
using System.Collections.Generic;
using System.Linq;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.RepositoryContract.City;
using Xtramile.Weather.MainApp.Service.City;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service.City
{
    public class CityServiceUnitTest
    {
        #region Fields

        private readonly Mock<ICityRepository> repositoryMock;
        private readonly CityService service;

        #endregion

        #region Constructor

        public CityServiceUnitTest()
        {
            repositoryMock = new Mock<ICityRepository>();
            service = new CityService(repositoryMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetById

        [Fact]
        public void GetById_Valid_ReturnCorrectValue()
        {
            PreparationGetById(true);

            var result = service.GetById(1);

            Assert.NotNull(result.Data);
            Assert.Equal("Jakarta", result.Data.Name);
            repositoryMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());

        }

        [Fact]
        public void GetById_Invalid_ReturnErrorMessage()
        {
            PreparationGetById(false);

            var result = service.GetById(0);

            Assert.Null(result.Data);
            Assert.Equal("Please insert valid Id", result.GetErrorMessage());
            repositoryMock.Verify(c => c.GetById(0), Times.Never());
        }

        [Fact]
        public void GetById_NotExists_ReturnNull()
        {
            PreparationGetById(false);

            var result = service.GetById(9);

            Assert.Null(result.Data);
            Assert.Equal("City with id 9 cannot be found.", result.GetErrorMessage());
            repositoryMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
        }

        #endregion

        #region GetByCountryId

        [Fact]
        public void GetByCountryId_ValidCountryId_ReturnListOfCountry()
        {
            PreparationGetByCountryId(true);

            var result = service.GetByCountryId(1);

            Assert.NotNull(result.DtoCollection);
            Assert.True(result.DtoCollection.Any());
            repositoryMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Once());

        }

        [Fact]
        public void GetByCountryId_InvalidCountryId_ReturnErrorMessage()
        {
            PreparationGetByCountryId(false);

            var result = service.GetByCountryId(0);

            Assert.True(!result.DtoCollection.Any());
            Assert.Equal("Please insert valid Id", result.GetErrorMessage());
            repositoryMock.Verify(c => c.GetByCountryId(0), Times.Never());
        }

        [Fact]
        public void GetByCountryId_NotExistsCountryId_ReturnEmptyListOfCountry()
        {
            PreparationGetByCountryId(false);

            var result = service.GetByCountryId(2);

            Assert.NotNull(result.DtoCollection);
            Assert.True(!result.DtoCollection.Any());
            repositoryMock.Verify(c => c.GetByCountryId(It.IsAny<int>()), Times.Once());

        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationGetById(bool isValid)
        {
            if (isValid)
            {
                var dto = new CityDto()
                {
                    Id = 1,
                    Name = "Jakarta",
                    CountryId = 1,
                };
                repositoryMock.Setup(item => item.GetById(It.IsAny<int>()))
                    .Returns(dto);
            }
            else
            {
                repositoryMock.Setup(item => item.GetById(It.IsAny<int>()))
                    .Returns((CityDto)null);
            }
        }

        private void PreparationGetByCountryId(bool isValid)
        {
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

                repositoryMock.Setup(item => item.GetByCountryId(It.IsAny<int>()))
                        .Returns(cities);
            }
            else
            {
                repositoryMock.Setup(item => item.GetByCountryId(It.IsAny<int>()))
                    .Returns(new List<CityDto>());
            }
        }

        #endregion
    }
}

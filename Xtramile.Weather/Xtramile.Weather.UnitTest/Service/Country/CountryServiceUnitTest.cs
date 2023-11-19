using Moq;
using System.Collections.Generic;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.RepositoryContract.Country;
using Xtramile.Weather.MainApp.Service.Country;
using Xunit;

namespace Xtramile.Weather.UnitTest.Service.Country
{
    public class CountryServiceUnitTest
    {
        #region Fields

        private readonly Mock<ICountryRepository> repositoryMock;
        private readonly CountryService service;

        #endregion

        #region Constructor

        public CountryServiceUnitTest()
        {
            repositoryMock = new Mock<ICountryRepository>();
            service = new CountryService(repositoryMock.Object);
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
            Assert.Equal("Indonesia", result.Data.Name);
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
            Assert.Equal("Country with id 9 cannot be found.", result.GetErrorMessage());
            repositoryMock.Verify(c => c.GetById(It.IsAny<int>()), Times.Once());
        }

        #endregion

        #region GetAll

        [Fact]
        public void GetAll_NoParameter_ReturnListOfCountry()
        {
            PreparationGetAll();

            var result = service.GetAll();

            Assert.NotNull(result.DtoCollection);
            Assert.True(result.DtoCollection.Count > 1);
            repositoryMock.Verify(c => c.GetAll(), Times.Once());

        }

        #endregion

        #endregion

        #region Preparation Methods

        private void PreparationGetById(bool isValid)
        {
            if (isValid)
            {
                var dto = new CountryDto()
                {
                    Id = 1,
                    Name = "Indonesia",
                };
                repositoryMock.Setup(item => item.GetById(It.IsAny<int>()))
                    .Returns(dto);
            }
            else
            {
                repositoryMock.Setup(item => item.GetById(It.IsAny<int>()))
                    .Returns((CountryDto)null);
            }
        }

        private void PreparationGetAll()
        {

            var countries = new List<CountryDto>
            {
                new CountryDto() { Id = 1, Name = "Indonesia" },
                new CountryDto() { Id = 2, Name = "Australia"},
                new CountryDto() { Id = 3, Name = "UK" }
            };

            repositoryMock.Setup(item => item.GetAll())
                    .Returns(countries);
        }

        #endregion
    }
}

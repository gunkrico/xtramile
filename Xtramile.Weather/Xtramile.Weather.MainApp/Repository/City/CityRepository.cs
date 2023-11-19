using System.Collections.Generic;
using System.Linq;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.RepositoryContract.City;

namespace Xtramile.Weather.MainApp.Repository.City
{
    public class CityRepository : ICityRepository
    {
        private readonly List<CityDto> cities = new List<CityDto>();

        public CityRepository()
        {
            cities = new List<CityDto>
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
        }

        public CityDto GetById(int id)
        {
            return cities.FirstOrDefault(c => c.Id == id);
        }

        public List<CityDto> GetByCountryId(int countryId)
        {
            return cities.Where(c => c.CountryId == countryId).ToList();
        }
    }
}

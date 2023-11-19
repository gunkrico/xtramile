using System.Collections.Generic;
using System.Linq;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.RepositoryContract.Country;

namespace Xtramile.Weather.MainApp.Repository.Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly List<CountryDto> countries = new List<CountryDto>();

        public CountryRepository()
        {
            countries = new List<CountryDto>
            {
                new CountryDto() { Id = 1, Name = "Indonesia" },
                new CountryDto() { Id = 2, Name = "Australia"},
                new CountryDto() { Id = 3, Name = "UK" }
            };
        }

        public CountryDto GetById(int id)
        {
            return countries.FirstOrDefault(c => c.Id == id);
        }

        public List<CountryDto> GetAll()
        {
            return countries;
        }
    }
}

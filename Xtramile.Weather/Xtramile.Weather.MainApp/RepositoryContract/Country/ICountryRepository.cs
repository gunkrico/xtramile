using System.Collections.Generic;
using Xtramile.Weather.MainApp.Dto.Country;

namespace Xtramile.Weather.MainApp.RepositoryContract.Country
{
    public interface ICountryRepository
    {
        CountryDto GetById(int id);

        List<CountryDto> GetAll();
    }
}

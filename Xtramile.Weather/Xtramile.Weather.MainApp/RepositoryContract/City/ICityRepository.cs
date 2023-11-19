using System.Collections.Generic;
using Xtramile.Weather.MainApp.Dto.City;

namespace Xtramile.Weather.MainApp.RepositoryContract.City
{
    public interface ICityRepository
    {
        CityDto GetById(int id);

        List<CityDto> GetByCountryId(int countryId);
    }
}

using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.ServiceContract.City
{
    public interface ICityService
    {
        GenericResponse<CityDto> GetById(int id);
        GenericGetDtoCollectionResponse<CityDto> GetByCountryId(int countryId);
    }
}

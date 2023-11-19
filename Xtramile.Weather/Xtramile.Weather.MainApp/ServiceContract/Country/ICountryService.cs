using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.ServiceContract.Country
{
    public interface ICountryService
    {
        GenericResponse<CountryDto> GetById(int id);
        GenericGetDtoCollectionResponse<CountryDto> GetAll();
    }
}

using System;
using System.Linq;
using Xtramile.Weather.MainApp.Dto.City;
using Xtramile.Weather.MainApp.RepositoryContract.City;
using Xtramile.Weather.MainApp.ServiceContract.City;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.Service.City
{
    public class CityService : ICityService
    {
        private readonly ICityRepository repository;

        public CityService(ICityRepository repository)
        {
            this.repository = repository;
        }

        public GenericResponse<CityDto> GetById(int id)
        {
            var response = new GenericResponse<CityDto>();

            try
            {
                if (id > 0)
                {
                    var dto = repository.GetById(id);

                    if (dto == null)
                    {
                        response.AddErrorMessage($"City with id {id} cannot be found.");
                        return response;
                    }

                    response.Data = dto;
                }
                else
                {
                    response.AddErrorMessage("Please insert valid Id");
                    return response;
                }
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public GenericGetDtoCollectionResponse<CityDto> GetByCountryId(int countryId)
        {
            var response = new GenericGetDtoCollectionResponse<CityDto>();
            try
            {
                if (countryId > 0)
                {
                    var dtos = repository.GetByCountryId(countryId);

                    if (dtos.Any())
                    {
                        foreach (var dto in dtos)
                        {
                            response.DtoCollection.Add(dto);
                        }
                    }
                    else
                    {
                        response.AddErrorMessage($"Country with id {countryId} has no city.");
                        return response;
                    }
                }
                else
                {
                    response.AddErrorMessage("Please insert valid Id");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }
    }
}

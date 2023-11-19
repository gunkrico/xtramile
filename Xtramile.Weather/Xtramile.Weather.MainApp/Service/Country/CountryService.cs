using System;
using System.Linq;
using Xtramile.Weather.MainApp.Dto.Country;
using Xtramile.Weather.MainApp.RepositoryContract.Country;
using Xtramile.Weather.MainApp.ServiceContract.Country;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.Service.Country
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository repository;

        public CountryService(ICountryRepository repository)
        {
            this.repository = repository;
        }

        public GenericResponse<CountryDto> GetById(int id)
        {
            var response = new GenericResponse<CountryDto>();

            try
            {
                if (id > 0)
                {
                    var dto = repository.GetById(id);

                    if (dto == null)
                    {
                        response.AddErrorMessage($"Country with id {id} cannot be found.");
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

        public GenericGetDtoCollectionResponse<CountryDto> GetAll()
        {
            var response = new GenericGetDtoCollectionResponse<CountryDto>();

            try
            {
                var dtos = repository.GetAll();

                if (dtos.Any())
                {
                    foreach (var dto in dtos)
                    {
                        response.DtoCollection.Add(dto);
                    }
                }
                else
                {
                    response.AddErrorMessage("Country cannot be found.");
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

using System.Net.Http;
using System.Threading.Tasks;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.ServiceContract
{
    public interface IApiService
    {
        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod);

        Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod, StringContent content);
    }
}

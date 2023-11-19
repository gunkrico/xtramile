using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xtramile.Weather.MainApp.Dto.OpenWeather;
using Xtramile.Weather.MainApp.ServiceContract;
using Xtramile.Weather.MainApp.ServiceContract.Response;

namespace Xtramile.Weather.MainApp.Service
{
    public class ApiService :IApiService
    {
        #region Public Methods

        public async Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod)
        {
            return await this.SendRequestAsync(requestUrl, httpMethod, null);
        }

        public async Task<GenericResponse<string>> SendRequestAsync(string requestUrl, string httpMethod, StringContent content)
        {
            var response = new GenericResponse<string>();
            try
            {
                using (var client = new HttpClient())
                {
#if DEBUG
                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif

                    using (var httpRequest = new HttpRequestMessage(new HttpMethod(httpMethod), requestUrl))
                    {
                        if (content != null)
                        {
                            httpRequest.Content = content;
                        }

                        var apiResponse = await client.SendAsync(httpRequest);
                        if (!apiResponse.IsSuccessStatusCode)
                        {
                            var stringResponse = await apiResponse.Content.ReadAsStringAsync();
                            var errorContent = JsonConvert.DeserializeObject<ApiErrorDto>(stringResponse);

                            response.AddErrorMessage(errorContent.Message);
                            return response;
                        }

                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        response.Data = apiContent;
                    }
                }
            }
            catch (Exception exception)
            {
                response.AddErrorMessage(string.Format("Failed to send request to {0} with message {1}", requestUrl, exception.Message));
            }

            return response;
        }

        #endregion
    }
}

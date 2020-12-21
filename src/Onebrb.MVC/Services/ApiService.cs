using Dawn;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Onebrb.MVC.Config;
using Onebrb.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Onebrb.MVC.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;

        public ApiService(IConfiguration configuration)
        {
            Guard.Argument(configuration, nameof(configuration)).NotNull();
            _configuration = configuration;
        }

        public async Task<BaseApiResponse<T>> HttpGetRequest<T>(string uri)
        {
            // Get from API
            var apiOptions = new ApiOptions();
            _configuration.GetSection(ApiOptions.Token).Bind(apiOptions);

            BaseApiResponse<T> httpResponse;

            using (var httpClient = new HttpClient())
            {
                //using (var response = await httpClient.GetAsync($"{apiOptions.BaseAddress}/api/items/{itemId}"))
                using (var response = await httpClient.GetAsync($"{apiOptions.BaseAddress}/{uri}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    httpResponse = JsonConvert.DeserializeObject<BaseApiResponse<T>>(apiResponse);

                    if (httpResponse == null)
                    {
                        // Todo: exceptions intercecptor
                        throw new Exception("Couldn't fetch item");
                    }
                }
            }

            return httpResponse;
        }
    }
}

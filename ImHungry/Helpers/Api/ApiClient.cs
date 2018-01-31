using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ImHungry.Helpers.Api
{
    public class ApiClient: IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            using (var content = new FormUrlEncodedContent(values))
            {
                var query = await content.ReadAsStringAsync();
                var requestUriWithQuery = string.Concat(requestUri, "?", query);
                var response = await httpClient.GetAsync(requestUriWithQuery);
                return response;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace ImHungry.Helpers.Api
{
    public abstract class ClientBase
    {
        private readonly IApiClient apiClient;

        protected ClientBase(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        protected async Task<TResponse> GetJsonDecodedContent<TResponse, TContentResponse>(string uri, params KeyValuePair<string, string>[] requestParameters)
            where TResponse : ApiResponse<TContentResponse>, new()
        {
            using (var apiResponse = await apiClient.GetFormEncodedContent(uri, requestParameters))
            {
                var response = await CreateJsonResponse<TResponse>(apiResponse);
                if(response.StatusIsSuccessful)
                    response.Data = Json.Decode<TContentResponse>(response.ResponseResult);
                return response;
            }
        }

        private static async Task<TResponse> CreateJsonResponse<TResponse>(HttpResponseMessage response) where TResponse : ApiResponse, new()
        {
            var clientResponse = new TResponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ResponseCode = response.StatusCode
            };
            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }
    }
}
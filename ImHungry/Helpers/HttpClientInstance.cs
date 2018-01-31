using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ImHungry.Helpers
{
    public static class HttpClientInstance
    {
        private const string BaseUri = "http://food2fork.com/";
        private static readonly HttpClient instance = new HttpClient { BaseAddress = new Uri(BaseUri) };

        public static HttpClient Instance
        {
            get { return instance; }
        }
    }
}
using System;
using System.Net.Http;

namespace PokeSpeare.Api.Http
{
    public static class HttpClientFactory
    {
        private static HttpClient _httpClient;

        public static HttpClient Create()
        {
            return _httpClient ?? (_httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(20)
            });
        }
    }
}
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokeSpeare.Api.Configuration;
using PokeSpeare.Api.Http;
using PokeSpeare.Api.Models.FunTranslations;

namespace PokeSpeare.Api.Repositories
{

    public class FunTranslationRepository : IFunTranslationRepository
    {
        private readonly IOptions<FunTranslationApiConfig> _config;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public FunTranslationRepository(IOptions<FunTranslationApiConfig> config, IHttpClientWrapper httpClientWrapper)
        {
            _config = config;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<FunTranslation> GetShakespeareTranslationAsync(string text)
        {
            var endpoint = string.Format(_config.Value.EndpointUrl, text);

            var response = await _httpClientWrapper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FunTranslation>(content);
            }

            return null;
        }
    }
}
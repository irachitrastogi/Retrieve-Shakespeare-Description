using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokeSpeare.Api.Configuration;
using PokeSpeare.Api.Http;
using PokeSpeare.Api.Models.PokeApi;

namespace PokeSpeare.Api.Repositories
{

    public class PokeApiRepository : IPokeApiRepository
    {
        private readonly IOptions<PokeApiConfig> _config;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public PokeApiRepository(IOptions<PokeApiConfig> config, IHttpClientWrapper httpClientWrapper)
        {
            _config = config;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<PokemonSpecies> GetPokemonAsync(string name)
        {
            var endpoint = string.Format(_config.Value.EndpointUrl, name);
            var response = await _httpClientWrapper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PokemonSpecies>(content);
            }

            return null;
        }
    }
}

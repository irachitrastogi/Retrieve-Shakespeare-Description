using Newtonsoft.Json;

namespace PokeSpeare.Api.Models.PokeApi
{
    public class FlavorTextEntries
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        public Language Language { get; set; }
    }
}
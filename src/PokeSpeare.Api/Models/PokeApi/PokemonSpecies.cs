using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeSpeare.Api.Models.PokeApi
{
    public class PokemonSpecies
    {
        [JsonProperty("flavor_text_entries")]
        public IList<FlavorTextEntries> FlavorTextEntries { get; set; }
    }
}

using System.Threading.Tasks;

namespace PokeSpeare.Api.Services
{
    public interface IPokeSpearService
    {
        Task<string> GetPokemonDescriptionAsync(string pokemonName);

        Task<string> GetShakespeareTranslationAsync(string description);
    }
}
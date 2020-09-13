using System.Threading.Tasks;
using PokeSpeare.Api.Models.PokeApi;

namespace PokeSpeare.Api.Repositories
{
    public interface IPokeApiRepository
    {
        Task<PokemonSpecies> GetPokemonAsync(string name);
    }
}

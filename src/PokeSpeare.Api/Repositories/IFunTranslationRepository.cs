using System.Threading.Tasks;
using PokeSpeare.Api.Models.FunTranslations;

namespace PokeSpeare.Api.Repositories
{
    public interface IFunTranslationRepository
    {
        Task<FunTranslation> GetShakespeareTranslationAsync(string text);
    }
}
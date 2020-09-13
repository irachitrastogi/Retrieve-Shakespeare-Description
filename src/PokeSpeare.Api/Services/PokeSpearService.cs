using System;
using System.Linq;
using System.Threading.Tasks;
using PokeSpeare.Api.Repositories;

namespace PokeSpeare.Api.Services
{
    public class PokeSpearService : IPokeSpearService
    {
        private const string DefaultLanguage = "en";
        private readonly IPokeApiRepository _polPokeApiRepository;
        private readonly IFunTranslationRepository _funTranslationRepository;

        public PokeSpearService(IPokeApiRepository polPokeApiRepository, IFunTranslationRepository funTranslationRepository)
        {
            _polPokeApiRepository = polPokeApiRepository;
            _funTranslationRepository = funTranslationRepository;
        }

        public async Task<string> GetPokemonDescriptionAsync(string pokemonName)
        {
            var pokemonSpecies = await _polPokeApiRepository.GetPokemonAsync(pokemonName.ToLower());
            return pokemonSpecies?.FlavorTextEntries.FirstOrDefault(fl => fl.Language.Name == DefaultLanguage)?.FlavorText;
        }

        public async Task<string> GetShakespeareTranslationAsync(string text)
        {
            var escaped = EscapeString(text);
            var funTranslation = await _funTranslationRepository.GetShakespeareTranslationAsync(escaped);
            return funTranslation?.Contents?.Translated;
        }

        private static string EscapeString(string text)
        {
            return Uri.EscapeUriString(text.Replace("\n", " "));
        }
    }
}
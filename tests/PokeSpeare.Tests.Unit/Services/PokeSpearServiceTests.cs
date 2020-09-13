using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using PokeSpeare.Api.Models.FunTranslations;
using PokeSpeare.Api.Models.PokeApi;
using PokeSpeare.Api.Repositories;
using PokeSpeare.Api.Services;
using Xunit;

namespace PokeSpeare.Tests.Unit.Services
{
    public class PokeSpearServiceTests
    {
        private readonly Fixture _fixture;
        private readonly PokeSpearService _sut;
        private readonly Mock<IPokeApiRepository> _pokeApiRepositoryMock;
        private Mock<IFunTranslationRepository> _funTranslationRepositoryMock;

        public PokeSpearServiceTests()
        {
            _fixture = new Fixture();
            _pokeApiRepositoryMock = new Mock<IPokeApiRepository>();
            _funTranslationRepositoryMock = new Mock<IFunTranslationRepository>();
            _sut = new PokeSpearService(_pokeApiRepositoryMock.Object, _funTranslationRepositoryMock.Object);
        }

        #region GetPokemonDescriptionAsync

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonDescription_AndPokemonExistsWithEnglishTranslation_ThenReturnsPokemonDescription(string pokemonName)
        {
            //Given
            var flavourTextEntries = _fixture.Build<FlavorTextEntries>()
                .With(x => x.Language, new Language
                {
                    Name = "en"
                }).Create();

            var pokemon = _fixture.Build<PokemonSpecies>()
                .With(x => x.FlavorTextEntries, new[] { flavourTextEntries })
                .Create();

            _pokeApiRepositoryMock.Setup(x => x.GetPokemonAsync(pokemonName.ToLower())).ReturnsAsync(pokemon);

            //When
            var description = await _sut.GetPokemonDescriptionAsync(pokemonName);

            //Then
            Assert.Equal(pokemon.FlavorTextEntries.First(x => x.Language.Name == "en").FlavorText, description);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonDescription_AndPokemonDoesNotExistsWithEnglishTranslation_ThenReturnsPokemonDescriptionNull(string pokemonName)
        {
            //Given
            var flavourTextEntries = _fixture.Build<FlavorTextEntries>()
                .With(x => x.Language, new Language
                {
                    Name = "it"
                }).Create();

            var pokemon = _fixture.Build<PokemonSpecies>()
                .With(x => x.FlavorTextEntries, new[] { flavourTextEntries })
                .Create();

            _pokeApiRepositoryMock.Setup(x => x.GetPokemonAsync(pokemonName.ToLower())).ReturnsAsync(pokemon);

            //When
            var description = await _sut.GetPokemonDescriptionAsync(pokemonName);

            //Then
            Assert.Null(description);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonDescription_AndPokemonDoesNotExists_ThenReturnsPokemonDescriptionNull(string pokemonName)
        {
            //Given
            _pokeApiRepositoryMock.Setup(x => x.GetPokemonAsync(pokemonName.ToLower())).ReturnsAsync((PokemonSpecies)null);

            //When
            var description = await _sut.GetPokemonDescriptionAsync(pokemonName);

            //Then
            Assert.Null(description);
        }

        #endregion

        #region GetShakespeareTranslationAsync

        [Theory]
        [AutoData]
        public async Task GivenPokemonDescription_WhenGetShakespeareTranslation_ThenShakespeareFlavourTranslationIsReturned(string description, FunTranslation funTranslation)
        {
            //Given
            _funTranslationRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(funTranslation);

            //When
            var translation = await _sut.GetShakespeareTranslationAsync(description);

            //Then
            Assert.Equal(funTranslation.Contents.Translated, translation);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonDescription_WhenGetShakespeareTranslation_AndFunTranslationIsNUll_ThenShakespeareFlavourTranslationIsReturnedNull(string description)
        {
            //Given
            _funTranslationRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync((FunTranslation)null);

            //When
            var translation = await _sut.GetShakespeareTranslationAsync(description);

            //Then
            Assert.Null(translation);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonDescription_WhenGetShakespeareTranslation_AndContentObjectIsNotNull_AndTranslatedPropertyIsNull_ThenShakespeareFlavourTranslationIsReturnedNull(string description)
        {
            //Given
            var contents = _fixture.Build<Contents>()
                .With(x => x.Translated, (string) null)
                .Create();

            var funTranslation = _fixture.Build<FunTranslation>()
                .With(x => x.Contents, contents)
                .Create();

            _funTranslationRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(funTranslation);

            //When
            var translation = await _sut.GetShakespeareTranslationAsync(description);

            //Then
            Assert.Null(translation);
        }

        #endregion
    }
}

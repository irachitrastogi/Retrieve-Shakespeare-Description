using System.Net;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokeSpeare.Api.Controllers;
using PokeSpeare.Api.Models;
using PokeSpeare.Api.Services;
using Xunit;

namespace PokeSpeare.Tests.Unit.Controllers
{
    public class PokemonControllerTests
    {

        private readonly PokemonController _sut;
        private readonly Mock<IPokeSpearService> _pokespearServiceMock;

        public PokemonControllerTests()
        {
            _pokespearServiceMock = new Mock<IPokeSpearService>();
            _sut = new PokemonController(_pokespearServiceMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonWithShakespeareDescription_Then200OKIsReturned_WithTranslatedDescription(
            string pokemonName, 
            string description, 
            string translatedDescription)
        {
            // given
            _pokespearServiceMock.Setup(x =>
                x.GetPokemonDescriptionAsync(pokemonName)).ReturnsAsync(description);
            _pokespearServiceMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(translatedDescription);

            // when
            var result = (ObjectResult)await _sut.GetPokemonWithShakespeareDescriptionAsync(pokemonName);
            var response = (GetPokemonResponse)result.Value;

            // then
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(pokemonName, response.Name);
            Assert.Equal(translatedDescription, response.Description);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonWithShakespeareDescription_AndPokemonDoesNotExist_Then404NotFoundIsReturned_WithPokemonNameOnly(
            string pokemonName)
        {
            // given
            _pokespearServiceMock.Setup(x =>
                x.GetPokemonDescriptionAsync(pokemonName)).ReturnsAsync(default(string));

            // when
            var result = (ObjectResult)await _sut.GetPokemonWithShakespeareDescriptionAsync(pokemonName);
            var response = (GetPokemonResponse)result.Value;

            // then
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(pokemonName, response.Name);
            Assert.Null(response.Description);
        }
    }
}

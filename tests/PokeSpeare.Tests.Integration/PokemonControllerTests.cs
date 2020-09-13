using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PokeSpeare.Api;
using PokeSpeare.Api.Controllers;
using PokeSpeare.Api.Models;
using PokeSpeare.Api.Services;
using Xunit;

namespace PokeSpeare.Tests.Integration
{
    public class PokemonControllerTests : IntegrationTestBase
    {
        private PokemonController _sut;

        public PokemonControllerTests()
        {
            var services = new ServiceCollection();
            new Startup(Configuration).ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            _sut = new PokemonController(serviceProvider.GetService<IPokeSpearService>());
        }

        [Theory]
        [InlineData("Charizard")]
        public async Task GivenPokemonName_WhenGetPokemonWithShakespeareDescription_TheReturns200OK(string pokemonName)
        {
            // When
            var response = (await _sut.GetPokemonWithShakespeareDescriptionAsync(pokemonName)) as ObjectResult;

            // Then
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(((GetPokemonResponse)response.Value).Description);
        }
    }
}

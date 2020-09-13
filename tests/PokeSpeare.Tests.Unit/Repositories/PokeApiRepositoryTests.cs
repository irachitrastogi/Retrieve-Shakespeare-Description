using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using PokeSpeare.Api.Configuration;
using PokeSpeare.Api.Http;
using PokeSpeare.Api.Models.FunTranslations;
using PokeSpeare.Api.Models.PokeApi;
using PokeSpeare.Api.Repositories;
using Xunit;

namespace PokeSpeare.Tests.Unit.Repositories
{
    public class PokeApiRepositoryTests
    {
        private PokeApiRepository _sut;
        private readonly Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private readonly IOptions<PokeApiConfig> _config;

        public PokeApiRepositoryTests()
        {
            _config = Options.Create(new PokeApiConfig()
            {
                EndpointUrl = "http://localhost/pokemon/{0}"
            });

            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _sut = new PokeApiRepository(_config, _httpClientWrapperMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonAsync_AndResponseIsSuccessfull_ThenReturnsPokemonSpecies(
            string pokemonName, PokemonSpecies pokemon)
        {
            //Given
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndpointUrl, pokemonName)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(pokemon))
                });

            //When
            var response = await _sut.GetPokemonAsync(pokemonName);

            //Then
            Assert.Equal(pokemon.FlavorTextEntries.Count, response.FlavorTextEntries.Count);
            pokemon.FlavorTextEntries.ToList().ForEach(fe => response.FlavorTextEntries.Contains(It.Is<FlavorTextEntries>(item => item.FlavorText == fe.FlavorText)));
        }

        [Theory]
        [AutoData]
        public async Task GivenPokemonName_WhenGetPokemonAsync_AndResponseIsNotSuccessfull_ThenReturnsNull(string pokemonName)
        {
            ///Given
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndpointUrl, pokemonName)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            //When
            var response = await _sut.GetPokemonAsync(pokemonName);

            //Then
            Assert.Null(response);
        }
    }
}

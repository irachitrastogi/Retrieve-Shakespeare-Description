using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokeSpeare.Api.Configuration;
using PokeSpeare.Api.Http;
using PokeSpeare.Api.Models.FunTranslations;
using PokeSpeare.Api.Repositories;
using Xunit;

namespace PokeSpeare.Tests.Unit.Repositories
{
    public class FunTranslationRepositoryTests
    {
        private readonly FunTranslationRepository _sut;
        private readonly Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private readonly IOptions<FunTranslationApiConfig> _config;

        public FunTranslationRepositoryTests()
        {
            _config = Options.Create(new FunTranslationApiConfig()
            {
                EndpointUrl = "http://localhost/funtranslations/{0}"
            });

            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _sut = new FunTranslationRepository(_config, _httpClientWrapperMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task GivenText_WhenGetShakespeareTranslation_AndResponseIsSuccessfull_ThenReturnsTranslation(string text, string translation)
        {
            //Given
            _httpClientWrapperMock
                .Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndpointUrl, text)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new FunTranslation
                    {
                        Contents = new Contents()
                        {
                            Translated = translation
                        }
                    }))
                });

            //When
            var response = await _sut.GetShakespeareTranslationAsync(text);

            //Then
            Assert.Equal(response.Contents.Translated, translation);
        }

        [Theory]
        [AutoData]
        public async Task GivenText_WhenGetShakespeareTranslation_AndResponseIsNotSuccessfull_ThenReturnsTranslationNull(string text)
        {
            //Given
            _httpClientWrapperMock
                .Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndpointUrl, text)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            //When
            var response = await _sut.GetShakespeareTranslationAsync(text);

            //Then
            Assert.Null(response);
        }

    }
}

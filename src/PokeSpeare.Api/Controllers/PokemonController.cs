using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeSpeare.Api.Middlewares;
using PokeSpeare.Api.Models;
using PokeSpeare.Api.Models.SwaggerExamples;
using PokeSpeare.Api.Services;
using Swashbuckle.AspNetCore.Examples;

namespace PokeSpeare.Api.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeSpearService _pokespearService;

        public PokemonController(IPokeSpearService pokespearService)
        {
            _pokespearService = pokespearService;
        }

        /// <summary>
        /// Get pokemon description in a Shakespeare flavour
        /// </summary>
        /// <param name="name">The pokemon name</param>
        /// <returns>The pokemon name and description in a Shakespeare flavour</returns>
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(typeof(GetPokemonResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(GetPokemonResponseExample))]
        public async Task<IActionResult> GetPokemonWithShakespeareDescriptionAsync(string name)
        { 
            var description = await _pokespearService.GetPokemonDescriptionAsync(name);

            if (string.IsNullOrEmpty(description))
            {
                return NotFound(new GetPokemonResponse
                {
                    Name = name
                });
            }

            var translation = await _pokespearService.GetShakespeareTranslationAsync(description);

            return Ok(new GetPokemonResponse
            {
                Name = name,
                Description = translation
            });
        }
    }
}

using Swashbuckle.AspNetCore.Examples;

namespace PokeSpeare.Api.Models.SwaggerExamples
{
    public class GetPokemonResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new GetPokemonResponse
            {
                Name = "Charizard",
                Description = "This is an example description"
            };
        }
    }
}
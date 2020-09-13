namespace PokeSpeare.Api.Models
{
    public class GetPokemonResponse
    {
        /// <summary>
        /// The pokemon name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The pokemon Shakespeare flavoured description
        /// </summary>
        public string Description { get; set; }
    }
}
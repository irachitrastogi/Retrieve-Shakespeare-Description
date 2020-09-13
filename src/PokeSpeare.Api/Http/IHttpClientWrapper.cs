using System.Net.Http;
using System.Threading.Tasks;

namespace PokeSpeare.Api.Http
{
    public interface IHttpClientWrapper
    {

        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PokeSpeare.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                {
                    ErrorMessage = "Ops, \'t seemeth something did wend wrong"
                }));
            }
        }
    }
}

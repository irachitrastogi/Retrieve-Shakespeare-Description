using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeSpeare.Api.Configuration;
using PokeSpeare.Api.Http;
using PokeSpeare.Api.Middlewares;
using PokeSpeare.Api.Repositories;
using PokeSpeare.Api.Services;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace PokeSpeare.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PokeApiConfig>(Configuration.GetSection("PokeApi"));
            services.Configure<FunTranslationApiConfig>(Configuration.GetSection("FunTranslationApi"));

            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IPokeApiRepository, PokeApiRepository>();
            services.AddTransient<IFunTranslationRepository, FunTranslationRepository>();
            services.AddTransient<IPokeSpearService, PokeSpearService>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<ExamplesOperationFilter>();
                options.SwaggerDoc("v1", new Info() { Title = "PokeSpeare API", Version = "v1" });

                try
                {
                    var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "PokeSpeare.Api.xml");

                    if (File.Exists(filePath))
                    {
                        options.IncludeXmlComments(filePath);
                        options.DescribeAllEnumsAsStrings();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>()
                .UseSwagger()
                .UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokespeare API v1"); })
                .UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

            app.UseMvc();
        }
    }
}

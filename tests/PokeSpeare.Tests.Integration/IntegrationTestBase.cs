using System.IO;
using Microsoft.Extensions.Configuration;

namespace PokeSpeare.Tests.Integration
{
    public class IntegrationTestBase
    {
        protected IConfigurationRoot Configuration;

        protected IntegrationTestBase()
        {
            Configuration = CreateConfigFile();
        }

        private IConfigurationRoot CreateConfigFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
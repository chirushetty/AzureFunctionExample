using Application;
using AzureFunctionExample;
using Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionExample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder) => ConfigureServices(builder.Services);

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            //services
            //    .AddJsonStreamSerializer(new JsonSerializerOptions()
            //    {
            //    });
            services
                .AddMvcCore();

            services.AddApplication();
            //services.Add
            services.AddInfrastructure()
                .AddOptions<CosmosUserRepositoryOptions>()
                .Configure<IConfiguration, ILogger<Startup>>((options, configuration, logger) =>
                {
                    var optionsTypeName = options.GetType().Name;
                    var (endpoint, key) = ExtractFromConnnectionString(configuration["CosmosDb::ConnectionString::Secret"]);
                    options.AccountEndpoint = endpoint;
                    options.AccountKey = key;
                    options.DatabaseName = configuration["CosmosDb::Database::Name"];
                })
                ;
        }

        private (string endpoint, string key) ExtractFromConnnectionString(string connectionString)
        {
            string[] vs = connectionString
                            .Replace("AccountEndpoint=", string.Empty)
                            .Replace("AccountKey=", string.Empty)
                            .Split(";");
            var (endpoint, key) = (vs[0],vs[1]);

            return (endpoint, key);
        }
    }
}

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
using AzureFunctionExample.Helpers;
using Domain;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionExample
{
    public class Startup : FunctionsStartup
    {
        

        public IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder) => ConfigureServices(builder.Services);

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = services.BuildServiceProvider();
            Configuration = builder.GetRequiredService<IConfiguration>();
            var val = Configuration["APPINSIGHT_INSTRUMENTATIONKEY"];
            services.AddLogging();

            
            services
                .AddMvcCore();

            services.AddApplication();


            services.AddOptions<DependenciesHealthCheck>()
                .Configure(s =>
                {
                    s.CheckAsync();
                });

            services.AddScoped<IHealthCheck, DependenciesHealthCheck>();
            services.AddOptions<CosmosUserRepositoryOptions>()
                .Configure(options =>
                {
                    var (endpoint, key) =
                        ExtractFromConnnectionString(Configuration["CosmosDb::ConnectionString::Secret"]);
                    var databaseName = Configuration["CosmosDb::Database::Name"];
                    options.AccountEndpoint = endpoint;
                    options.AccountKey = key;
                    options.DatabaseName = databaseName;
                });

            services.AddInfrastructure();
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

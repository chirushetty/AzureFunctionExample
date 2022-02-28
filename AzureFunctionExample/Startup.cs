using Application;
using AzureFunctionExample;
using Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        

        public IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder) => ConfigureServices(builder.Services);

        private void ConfigureServices(IServiceCollection services)
        {
            var builder = services.BuildServiceProvider();
            Configuration = builder.GetRequiredService<IConfiguration>();
            var val = Configuration["APPINSIGHT_INSTRUMENTATIONKEY"];
            services.AddLogging();

            //services
            //    .AddJsonStreamSerializer(new JsonSerializerOptions()
            //    {
            //    });
            services
                .AddMvcCore();

            services.AddApplication();
            //services.Add
            //services.AddInfrastructure()
            //    .AddOptions<CosmosUserRepositoryOptions>()
            //    .Configure<IConfiguration, ILogger<Startup>>((options, configuration, logger) =>
            //    {
            //        var optionsTypeName = options.GetType().Name;
            //        var (endpoint, key) = ExtractFromConnnectionString(configuration["CosmosDb::ConnectionString::Secret"]);
            //        options.AccountEndpoint = endpoint;
            //        options.AccountKey = key;
            //        options.DatabaseName = configuration["CosmosDb::Database::Name"];
            //    })
            //    ;

            services.AddSingleton<CosmosUserRepositoryOptions>(sp =>
            {
                var val = 1;
                var (endpoint, key) = ExtractFromConnnectionString(Configuration["CosmosDb::ConnectionString::Secret"]);
                var databaseName = Configuration["CosmosDb::Database::Name"];

                return new CosmosUserRepositoryOptions(endpoint, key, databaseName);
            });

            services.AddInfrastructure();
            services.AddHealthChecks().AddCheck<HealthCheck>("Sample",
        failureStatus: HealthStatus.Degraded,
        tags: new[] { "sample" });
            //.AddDbContextCheck<DatabaseContext>();


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

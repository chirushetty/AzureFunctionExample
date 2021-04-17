using Application;
using AzureFunctionExample;
using Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddInfrastructure();
        }
    }
}

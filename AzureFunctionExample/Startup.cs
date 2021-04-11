using Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            //services.AddInfrastructure();
        }
    }
}

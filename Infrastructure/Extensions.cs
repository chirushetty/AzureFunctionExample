using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    //public static class Extensions
    //{ 
    //    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    //    {
    //        services.AddEntityFrameworkCosmos()
    //            .AddDbContext<DatabaseContext>((sp, builder) =>
    //            {
    //                if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment()) 
    //                {
    //                    builder.EnableDetailedErrors();
    //                    builder.EnableSensitiveDataLogging();
    //                }
    //            });

    //        services.AddScoped<IUserRepository, CosmosUserRepository>();

    //        return services;
    //    }
    //}

    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddEntityFrameworkCosmos()
                .AddDbContext<DatabaseContext>((sp, builder) =>
                {
                    if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment()) 
                    {
                        builder.EnableDetailedErrors();
                        builder.EnableSensitiveDataLogging();
                    }
                });
            services.AddScoped<IUserRepository, CosmosUserRepository>();
            return services;
        }
    }
}

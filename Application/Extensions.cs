using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddTransient<ICreateUserCommandHandler, CreateUserCommandHandler>();
            return services;
        }
    }
}

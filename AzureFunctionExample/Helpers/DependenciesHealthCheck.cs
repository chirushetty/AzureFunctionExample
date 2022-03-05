using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AzureFunctionExample.Models;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionExample.Helpers
{
    public class DependenciesHealthCheck : IHealthCheck
    {
        public IServiceProvider _serviceProvider;

        public DependenciesHealthCheck(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public HealthCheckResult CheckAsync()
        {
            var healthCheckResult = new HealthCheckResult
            {
                Message = "Critical dependencies loaded sucessfully",
                Name = "Critical dependencies builder health check",
                Status = CheckStatus.Healthy
            };

            try
            {
                _serviceProvider.GetRequiredService<IUserRepository>();
            }
            catch (Exception ex)
            {
                healthCheckResult.Status = CheckStatus.Unhealthy;
                healthCheckResult.Message = $"Unable to load critical dependencies: {ex.Message}";
            }

            return healthCheckResult;
            //return Task.FromResult(healthCheckResult);
        }

        
    }
}

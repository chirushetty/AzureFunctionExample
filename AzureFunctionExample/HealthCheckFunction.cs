using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AzureFunctionExample.Helpers;
using AzureFunctionExample.Models;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionExample
{
    public class HealthCheckFunction
    {
        private readonly ILogger<HealthCheckFunction> _logger;
        private readonly IHealthCheck _healthCheck;

        public HealthCheckFunction(ILogger<HealthCheckFunction> logger, IHealthCheck healthCheck)
        {
            _logger = logger;
            _healthCheck = healthCheck;
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
                _healthCheck.CheckAsync();
            }
            catch (Exception ex)
            {
                healthCheckResult.Status = CheckStatus.Unhealthy;
                healthCheckResult.Message = $"Unable to load critical dependencies: {ex.Message}";
            }

            return healthCheckResult;
        }

        [FunctionName(nameof(HealthCheckFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, methods: "GET", Route = "HealthCheck")]
            HttpRequest req)
        {
            _logger.LogDebug("Starting healthcheck");
            var check = CheckAsync();
            _logger.LogDebug("Healthcheck complete");
            return new OkObjectResult(check);
        }
    }
}

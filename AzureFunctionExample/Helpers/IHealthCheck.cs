using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureFunctionExample.Models;

namespace AzureFunctionExample.Helpers
{
    public interface IHealthCheck
    {
        HealthCheckResult CheckAsync();
    }
}

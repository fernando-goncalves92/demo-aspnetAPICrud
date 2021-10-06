using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Api.HealthChecks
{
    public class RandomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            int randomResult = new Random().Next(1, 10);

            if (randomResult % 2 == 0)
                return HealthCheckResult.Healthy();
            else
                return HealthCheckResult.Unhealthy();
        }
    }
}

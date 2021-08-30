using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.HealthCheck
{
    public class APIHealthCheck:IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;
            if (healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Employee BonusPool Api is running OK."));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("Health check status is unhealthy for Employee BonusPool Api."));
        }
    }
}

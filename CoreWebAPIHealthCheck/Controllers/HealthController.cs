using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIHealthCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("GetHealthStatistics")]
        public IActionResult GetHealthStatistics()
        {
            var healthStats = HealthCheckMiddleware.GetHealthStats();

            return Ok(healthStats);
        }
    }
}

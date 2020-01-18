using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers
{
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok("Agent Portal running");
        }
    }
}
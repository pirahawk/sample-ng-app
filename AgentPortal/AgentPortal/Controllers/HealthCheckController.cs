using AgentPortal.Db;
using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers
{
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {

        public HealthCheckController(AgentPortalDBContext context)
        {
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok("Agent Portal running");
        }
    }
}
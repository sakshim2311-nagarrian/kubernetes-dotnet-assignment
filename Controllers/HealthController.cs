using Microsoft.AspNetCore.Mvc;

namespace ServiceApiTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // GET: api/health
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                service = "ServiceApiTier"
            });
        }
    }
}
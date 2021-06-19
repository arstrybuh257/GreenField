using Microsoft.AspNetCore.Mvc;

namespace GreenField.IoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("IOT");
        }
    }
}
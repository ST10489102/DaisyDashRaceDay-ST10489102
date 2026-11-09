using Microsoft.AspNetCore.Mvc;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Daisy Dash is working! Pink & Grey Swagger is alive!";
        }

        [HttpGet("horses")]
        public List<string> GetHorses()
        {
            return new List<string> { "Lightning", "Sparkle", "Daisy" };
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/command/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
                
        }

        [HttpGet]
        public IActionResult TestInbound()
        {
            Console.WriteLine("Inbound request works on command service....");
            return Ok("Inbound request works on command service....");
        }
    }
}

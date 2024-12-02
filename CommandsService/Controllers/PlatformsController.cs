using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [ApiController]
  [Route("api/c/[controller]")]
  public class PlatformController : ControllerBase
  {
    public PlatformController()
    {

    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command service");

      return Ok("Inbound Test of Platform Controller");

    }
  }
}

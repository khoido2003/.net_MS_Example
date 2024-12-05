using AutoMapper;
using CommandsService.Data;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [ApiController]
  [Route("api/c/[controller]")]
  public class PlatformController : ControllerBase
  {
    private readonly ICommandRepo _repository;
    private readonly IMapper _mapper;

    public PlatformController(ICommandRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command service");

      return Ok("Inbound Test of Platform Controller");

    }
  }
}

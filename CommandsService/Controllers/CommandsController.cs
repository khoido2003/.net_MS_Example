using System.Collections;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [ApiController]
  [Route("api/c/platforms/{platformId}/[controller]")]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
      Console.WriteLine($"--> Get Commands for Platform: {platformId}");

      if (!_repo.PlatformExists(platformId))
      {

        Console.WriteLine($"--> not found Commands for Platform: {platformId}");

        return NotFound();
      }

      var commands = _repo.GetCommandsForPlatform(platformId);

      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));

    }

    [HttpGet("{commandId}", Name = "GetCommandsForPlatform")]
    public ActionResult<CommandReadDto> GetCommandsForPlatform(int platformId, int commandId)
    {
      Console.WriteLine($"--> Get Commands for Platform: {platformId} / {commandId}");

      if (!_repo.PlatformExists(platformId))
      {
        Console.WriteLine("Not found " + platformId + " " + commandId);
        return NotFound();
      }

      var command = _repo.GetCommand(platformId, commandId);

      if (command == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(_mapper.Map<CommandReadDto>(command));
      }
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
    {

      Console.WriteLine($"--> POST Commands for Platform: {platformId} ");
      if (!_repo.PlatformExists(platformId))
      {
        return NotFound();
      }

      var command = _mapper.Map<Command>(commandDto);

      _repo.CreateCommand(platformId, command);
      _repo.SaveChanges();

      var commandReadDto = _mapper.Map<CommandReadDto>(command);


      return CreatedAtRoute(nameof(GetCommandsForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);

    }

  }
}











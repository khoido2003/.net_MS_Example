using CommandsService.Models;

namespace CommandsService.Data
{
  public class CommandsRepo : ICommandRepo
  {

    private readonly AppDbContext _context;

    public CommandsRepo(AppDbContext context)
    {
      _context = context;
    }

    public void CreateCommand(int platformId, Command command)
    {
      if (command != null)
      {
        command.PlatformId = platformId;
        _context.Commands.Add(command);
      }
      else
      {
        throw new ArgumentNullException(nameof(command));
      }
    }

    public void CreatePlatform(Platform plat)
    {
      if (plat != null)
      {
        _context.Platforms.Add(plat);
      }
      else
      {
        throw new ArgumentNullException();
      }
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _context.Platforms.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
      return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault()!;
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
      return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
    }

    public bool PlatformExists(int platformId)
    {
      return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
      return _context.SaveChanges() >= 0;
    }
  }
}

using System.Collections;
using PlatformService.Models;

namespace PlatformService.Data
{
  public interface IPlatformRepo
  {
    Task<bool> SaveChanges();

    Task<IEnumerable<Platform>> GetAllPlatforms();

    Task<Platform> GetPlatformById(int id);

    void CreatePlatform(Platform plat);


  }
}

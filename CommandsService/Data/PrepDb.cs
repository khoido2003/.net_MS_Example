using System.Collections;
using CommandsService.Models;
using CommandsService.SyncDataService.Grpc;

namespace CommandsService.Data
{
  public static class PrepDB
  {
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
        var platforms = grpcClient.
        ReturnAllPlatforms();

        SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
      }
    }
    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
      Console.WriteLine("--> Seeding data new pl,atforms...");

      foreach (var plat in platforms)
      {
        if (!repo.ExternalPlatformExist(plat.ExternalID))
        {

          repo.CreatePlatform(plat);
        }

        repo.SaveChanges();
      }
    }
  }

}

using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {

        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, isProd);
      }

    }
    private static void SeedData(AppDbContext context, bool isProd)
    {

      if (isProd)
      {
        Console.WriteLine("-> Attempting to apply migration.");

        try
        {
          context.Database.Migrate();
        }
        catch (Exception e)
        {
          Console.WriteLine($"Could not run migration: {e.Message}");
        }

      }


      if (!context.Platforms.Any())
      {

        Console.WriteLine("--> We are seeding data");

        context.Platforms.AddRange(
            new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
            new Platform()
            {
              Name = "SQL Server",
              Publisher = "Microsoft",
              Cost = "Free"
            },
            new Platform()
            {
              Name = "Oracle",
              Publisher = "Oracle",
              Cost = "Free"
            },
            new Platform()
            {
              Name = "MySQL",
              Publisher = "MySQL",
              Cost = "Free",
            }
      );

        context.SaveChanges();
      }
      else
      {

        Console.WriteLine("--> We already have data");
      }
    }
  }
}

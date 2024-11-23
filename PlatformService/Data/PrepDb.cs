using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {

        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
      }

    }
    private static void SeedData(AppDbContext context)
    {
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

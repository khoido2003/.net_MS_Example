using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>(
    provider =>
    {
      var configuration = provider.GetRequiredService<IConfiguration>();
      var client = new MessageBusClient(configuration);

      // Initialize the RabbitMQ connection
      client.InitializeConnectionAsync().GetAwaiter().GetResult();

      return client;
    }
    );

builder.Services.AddGrpc();

var configuration = builder.Configuration;
var env = builder.Environment;

Console.WriteLine($"--> CommandService endpoint {configuration["CommandService"]}");

// builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PlatformConn")));

if (env.IsProduction())
{
  Console.WriteLine("--> Using Sql Server");

  builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PlatformConn")));
}
else
{
  Console.WriteLine("--> Using  InMemDb");
  builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Inmem"));
}

//////////////////////////////////////
///

var app = builder.Build();

PrepDb.PrepPopulation(app, env.IsProduction());

app.Use(async (context, next) =>
{
  Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");
  await next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/proto/platforms.proto", async context =>
{
  await context.Response.WriteAsync(File.ReadAllText("Proto/platforms.proto"));
});


app.Run();


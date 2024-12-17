using System.Collections;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataService.Grpc
{
  public class PlatformDataClient : IPlatformDataClient
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    public IEnumerable<Platform> ReturnAllPlatforms()
    {
      Console.WriteLine("--> Calling GRPC Service " + _configuration["GrpcPlatform"]);

      var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
      var client = new GrpcPlatform.GrpcPlatformClient(channel);

      var request = new GetAllRequest();

      try { 

        var reply = client.GetAllPlatform(request);

        return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
      }
      catch (Exception e)
      {

        Console.WriteLine("--> could not grpc server: " + e.Message);
return null;
      }

    }
  }

}

using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
  public interface IMessageBusClient
  {
    Task PublishNewPlatform(PlatformPublishDto platformPublishDto);
  }
}

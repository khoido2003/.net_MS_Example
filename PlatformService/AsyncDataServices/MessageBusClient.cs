using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.AsyncDataServices
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IChannel _channel;


    public MessageBusClient(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task InitializeConnectionAsync()
    {

      var factory = new ConnectionFactory()
      {
        HostName = _configuration["RabbitMQHost"]!,
        Port = int.Parse(_configuration["RabbitMQPort"]!)
      };

      try
      {
        _connection = await factory.CreateConnectionAsync();

        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);

        // Attach the shutdown handler asynchronously
        _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdownAsync;

        Console.WriteLine("--> Connect to Message bus");

      }
      catch (Exception e)
      {
        Console.WriteLine("--> Could not connect to Message Bus: " + e);
      }
    }

    /////////////////////////////////////

    public async Task PublishNewPlatform(PlatformPublishDto platformPublishDto)
    {
      var message = JsonSerializer.Serialize(platformPublishDto);

      if (_connection.IsOpen)
      {
        Console.WriteLine("--> RabbitMq open, sending message....");

        // Send the messages
   await     SendMessage(message);

        await Task.CompletedTask;
      }
      else
      {
        Console.WriteLine("--> RabiitMq connection closed, not sending...");

        await Task.CompletedTask;
      }

    }

    private async Task SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);
      var props = new BasicProperties();

      await _channel.BasicPublishAsync(exchange: "trigger", routingKey: "", mandatory: false, basicProperties: props, body: body);

      Console.WriteLine($"--> We have sent {message}");
    }

    public async Task Dispose()
    {
      Console.WriteLine("Message bus disposed");
      if (_channel.IsOpen)
      {
        await _channel.CloseAsync();
        await _channel.CloseAsync();
      }
    }

    ////////////////////////////////////////

    private async Task RabbitMQ_ConnectionShutdownAsync(object sender, ShutdownEventArgs e)
    {
      Console.WriteLine("--> RabbitMq connection shutdown" + e.ReplyText);

      await Task.CompletedTask;
    }
  }
}



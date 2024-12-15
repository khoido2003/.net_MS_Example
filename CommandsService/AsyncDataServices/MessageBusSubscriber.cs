
using System.Text;
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices
{
  public class MessageBusSubscriber : BackgroundService
  {
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IChannel _channel;
    private string _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
      _configuration = configuration;
      _eventProcessor = eventProcessor;
    }

    private async Task InitializeRabbitMq()
    {
      try
      {
        var factory = new ConnectionFactory()
        {
          HostName = _configuration["RabbitMQHost"],
          Port = int.Parse(_configuration["RabbitMQPort"]),
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);


        var queueDeclareOk = await Task.Run(() => _channel.QueueDeclareAsync());

        // Extract the queue name
        _queueName = queueDeclareOk.QueueName;


        await _channel.QueueBindAsync(queue: _queueName, exchange: "trigger", routingKey: "");

        Console.WriteLine("--> Listenn on the message bus");
        _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

      }
      catch (Exception e)
      {
        Console.WriteLine("Somethingw went wrong: " + e.Message);
      }
    }

    private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
    {
      Console.WriteLine("--> connection shutdow");
    }

    public override async void Dispose()
    {
      Console.WriteLine("Message bus disposed");
      if (_channel.IsOpen)
      {
        await _channel.CloseAsync();
        await _channel.CloseAsync();
      }
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

      // Connect to rabbitMq
      await InitializeRabbitMq();

      stoppingToken.ThrowIfCancellationRequested();

      var consumer = new AsyncEventingBasicConsumer(_channel);

      consumer.ReceivedAsync += async (ModuleHandle, ea) =>
      {
        Console.WriteLine("--> Event received");

        var body = ea.Body;

        var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

        await Task.Run(() => _eventProcessor.ProcessEvent(notificationMessage));

      };
      await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
    }

  }
}

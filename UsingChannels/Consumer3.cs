// Ignore Spelling: app

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsingChannels;

public sealed class Consumer3 : BackgroundService
{
    private readonly ILogger _logger;
    private readonly Channel<GameServerEvent> _channel;

    public Consumer3(
        Channel<GameServerEvent> channel,
        ILogger<Consumer3> logger)
    {
        _channel = channel;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Process incoming messages from the Channel
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            // get the message (of type ChannelRequest)
            var request = await _channel.Reader.ReadAsync(stoppingToken);

            // simulated processing time
            //await Task.Delay(2000, stoppingToken);


            var msgForConsole = $"{nameof(Consumer3)}: {request.MessageBody}";

            // Show received message, and when possible the number of msg left in the queue
            if (_channel.Reader.CanCount)
            {
                var cnt = _channel.Reader.Count;
                Console.WriteLine($"Number of msg left in queue is: {cnt}");
            }
            Console.WriteLine(msgForConsole);
        }
    }
}

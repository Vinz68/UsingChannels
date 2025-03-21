using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using static UsingChannels.GameServerEvent;

namespace UsingChannels;

public sealed class NetworkService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly Channel<GameServerEvent> _channel;

    public NetworkService(
        Channel<GameServerEvent> channel,
        ILogger<Consumer3> logger)
    {
        _channel = channel;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cnt = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);

            var gsEvent = new GameServerEvent
            {
                Action = ActionEnum.ServerMessage,
                MessageBody = $"ServerMessage {cnt}, NetworkService says hello at {DateTime.Now}"
            };
            cnt++;

            // Push new message into the Channel 
            await _channel.Writer.WriteAsync(gsEvent, stoppingToken);
        }
    }

}

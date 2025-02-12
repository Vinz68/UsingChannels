using System.Reflection.PortableExecutable;
using System.Threading.Channels;

namespace UsingChannelsInWebAPI;

public class Processor : BackgroundService
{
    private readonly Channel<ChannelRequest> _channel;

    public Processor(Channel<ChannelRequest> channel)
    {
        _channel = channel;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Process incoming messages from the Channel
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            // get the message (of type ChannelRequest)
            var request = await _channel.Reader.ReadAsync(stoppingToken);

            // simulated processing time
            await Task.Delay(2000, stoppingToken);

            // Show received message, and when possible the number of msg left in the queue
            if (!_channel.Reader.CanCount)
            {
                Console.WriteLine(request.Message);
            }
            else
            {
                // TODO: My code never reaches this / investigate how we can use the Count.
                var cnt = _channel.Reader.Count;
                Console.WriteLine($"{request.Message}. Number of msg left in queue is: {cnt - 1}");
            }
        }

    }
}

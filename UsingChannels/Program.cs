using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace UsingChannels;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddHostedService<Consumer1>();
        builder.Services.AddHostedService<Consumer2>();
        builder.Services.AddHostedService<Consumer3>();
        builder.Services.AddHostedService<NetworkService>();

        // Register our Channel, distributing GameServerEvent (=type of messages in the channel)
        builder.Services.AddSingleton<Channel<GameServerEvent>>(
            _ => Channel.CreateUnbounded<GameServerEvent>(new UnboundedChannelOptions
            {
                SingleWriter = true,
                SingleReader = false,
                AllowSynchronousContinuations = false
            }));

        using IHost host = builder.Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Host created.");

        await host.RunAsync();
    }
}

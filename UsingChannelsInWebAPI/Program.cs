using UsingChannelsInWebAPI;
using System.Threading.Channels;


/// <summary>
/// Build and Run or Debug, use link "http://localhost:5276/send" to push message into the channel.
/// Push/send multiple times, and see them all being processed.
/// 
/// This example is copied from here: https://www.youtube.com/watch?v=lHC38t1w9Nc
/// </summary>

var builder = WebApplication.CreateBuilder(args);

// Register our consumer: a background service "Processor"
builder.Services.AddHostedService<Processor>();

// Register our Channel, distributing ChannelRequests (=type of messages in the channel)
builder.Services.AddSingleton<Channel<ChannelRequest>>(
    _ => Channel.CreateUnbounded<ChannelRequest>(new UnboundedChannelOptions
    {
        SingleReader = true,
        AllowSynchronousContinuations = false
    }));

var app = builder.Build();

app.MapGet("send", async (Channel<ChannelRequest> channel) =>
{
    // Push new message into the Channel 
    await channel.Writer.WriteAsync( new ChannelRequest($"Hello from {DateTime.UtcNow}"));
    return Results.Ok();
});


app.Run();



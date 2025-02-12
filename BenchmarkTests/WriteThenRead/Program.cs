using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading.Channels;

namespace UsingChannels;

/// <summary>
/// Build Release and Run (no Debug) to see the performance an minimal memory footprint,
/// where first an item is written in the channel and then read back
/// 
/// This example is copied from here: https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/
/// </summary>


[MemoryDiagnoser]
public class Program
{
    static void Main() => BenchmarkRunner.Run<Program>();

    private readonly Channel<int> s_channel = Channel.CreateUnbounded<int>();

    [Benchmark]
    public async Task WriteThenRead()
    {
        ChannelWriter<int> writer = s_channel.Writer;
        ChannelReader<int> reader = s_channel.Reader;
        for (int i = 0; i < 10_000_000; i++)
        {
            writer.TryWrite(i);
            await reader.ReadAsync();
        }
    }
}



using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading.Channels;

namespace UsingChannels;

/// <summary>
/// Build Release and Run (no Debug) to see the performance an minimal memory footprint,
/// where first an item is (tried) to read, but later written in the channel 
/// (so must always wait until something comes available)
/// 
/// This example is copied from here: https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/
/// </summary>

[MemoryDiagnoser]
public class Program
{
    static void Main() => BenchmarkRunner.Run<Program>();

    private readonly Channel<int> s_channel = Channel.CreateUnbounded<int>();

    [Benchmark]
    public async Task ReadThenWrite()
    {
        ChannelWriter<int> writer = s_channel.Writer;
        ChannelReader<int> reader = s_channel.Reader;
        for (int i = 0; i < 10_000_000; i++)
        {
            ValueTask<int> vt = reader.ReadAsync();
            writer.TryWrite(i);
            await vt;
        }
    }
}


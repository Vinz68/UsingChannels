# UsingChannels
Examples and experiments using System.Threading.Channels in C#

This repo shows how powerful and fast C# Channels are and how they can be used in simple and complex asynchronous scenarios. Channels provide a robust way to coordinate and communicate between threads and tasks in C# applications.
It helps to make code maintainable and clean.

## What are Channels ?
Channels are an implementation of the producer/consumer conceptual programming model. In this programming model, producers asynchronously produce data, and consumers asynchronously consume that data. In other words, this model passes data from one party to another through a first-in first-out ("FIFO") queue.
Multiple producers and/or consumers are possible. The number of messages in the queue can be unlimited (unbounded, up to memory is full) or bounded to a max number of messages.


## Used and useful Links
- [System.Threading.Channels library, by Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/channels)
- [The Most Underrated .NET Feature You Must Use, by Nick Chapsas](https://www.youtube.com/watch?v=lHC38t1w9Nc)
- [An Introduction to System.Threading.Channels, by Stephen Toub](https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/)

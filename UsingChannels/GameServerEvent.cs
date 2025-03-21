using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingChannels;


/// <summary>
/// The message in the channel
/// </summary>
public class GameServerEvent
{
    public enum ActionEnum
    {
        PlayerConnected,
        PlayerDisconnected,
        PlayerStatusUpdate,
        ScoreUpdate,
        ServerMessage,
        ServerShutdown
    }

    public ActionEnum Action { get; set; }
    public string? MessageBody { get; set; }
}




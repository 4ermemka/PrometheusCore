using Assets.Scripts.Shared.Tools;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts.Shared.Networking
{
    public static class NetMessagesParser
    {
        public static NetMsg ParseBytes(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);

            NetMsg msg = (NetMsg)formatter.Deserialize(ms);
            return msg;
        }
    }

    public class NetMessageHandler
    { 
        public NetMessageHandler() 
        { 
            
        }

        public void Handle(NetMsg msg)
        {
            switch (msg.OP)
            {
                case NetOP.UpdatePlayer:
                    Net_UpdatePlayer updatePlayerMessage = (msg as Net_UpdatePlayer);

                    LogInfo($"Received playerString: {updatePlayerMessage.PlayerString}");
                    break;
            }
        }

        public void LogInfo(string message)
        {
            ConsoleLogger.LogInformation(this.GetType().Name, message);
        }
    }
}

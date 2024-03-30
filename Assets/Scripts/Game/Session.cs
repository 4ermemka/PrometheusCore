using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Session
{
    public Action<string> OnLog;
    public Action<int, Player> OnNewPlayer;
    private Dictionary<int, Player> Players;
    private JsonSerializerSettings jsonSettings;

    public void Start()
    {
        jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        Players = new Dictionary<int, Player>();
    }

    public void AddPlayer(int id)
    {
        Player player = new Player($"NewPlayer[{id}]", 0, 16);
        Players.Add(id, player);
        OnNewPlayer?.Invoke(id, player);
    }
}

using System;
using System.Collections.Generic;

public static class Session
{
    public static Action<string> OnLog;
    private static Dictionary<int, Player> Players;

    public static void Start()
    {
        Players = new Dictionary<int, Player>();
    }

    public static void AddPlayer(int id)
    { 
        
    }
}

using Assets.Scripts.Shared.Constants;
using Assets.Scripts.Shared.Tools;
using Assets.Scripts.Shared.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    #region TestMode

    [SerializeField]
    private bool TestMode;

    [SerializeField]
    private GameObject ViewPlayerPrefab;

    [SerializeField]
    private GameObject ViewPlayerTestPrefab;

    [SerializeField]
    private List<SimpleViewConsole> LogEndpoints;

    #endregion

    public static GameManager Instance;
    private Server _server;
    private Session Session;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
        #region Server

        ConsoleLogger.OnLogInformation += LogInformation;
        ConsoleLogger.OnLogWarning += LogWarning;
        ConsoleLogger.OnLogError += LogError;
        ConsoleLogger.OnLogException += LogException;

        _server = new Server();
        _server.Start();

        #endregion

        #region Statics

        PlayerEffects.Configigurate();

        #endregion

        #region Session

        Session = new Session();
        Session.Start();

        #endregion
    }

    public void LogInformation(string source, string message)
    {
        Debug.Log($"[{source}][INF] {message}");
        if (LogEndpoints != null && LogEndpoints.Count > 0)
        {
            foreach (var console in LogEndpoints)
            {
                console.LogInformation(source, message);
            }
        }
    }

    public void LogWarning(string source, string message)
    {
        Debug.LogWarning($"[{source}][WRN] {message}");

        if (LogEndpoints != null && LogEndpoints.Count > 0)
        {
            foreach (var console in LogEndpoints)
            {
                console.LogWarning(source, message);
            }
        }
    }

    public void LogError(string source, string message)
    {
        Debug.LogError($"[{source}][INF] {message}");
        if (LogEndpoints != null && LogEndpoints.Count > 0)
        {
            foreach (var console in LogEndpoints)
            {
                console.LogError(source, message);
            }
        }
    }

    public void LogException(string source, Exception ex)
    {
        Debug.Log($"[{source}] EXCEPTION: {ex.Message},\n TRACE: {ex.StackTrace}");
        if (LogEndpoints != null && LogEndpoints.Count > 0)
        {
            foreach (var console in LogEndpoints)
            {
                console.LogException(source, ex);
            }
        }
    }

    public void Send(Player player)
    {
        Net_UpdatePlayer msg = new Net_UpdatePlayer()
        {
            OP = NetOP.UpdatePlayer,
            PlayerString = player.ToString()
        };

        byte[] buffer = new byte[4096];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        _server.SendClient(0, buffer);
    }

    public void OnDestroy()
    {
        _server?.Stop();
        Debug.Log($"Stopped client");
    }
}

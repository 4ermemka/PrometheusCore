using System.Net;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    private Server _server;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _server = new Server();
        _server.OnLog += LogInfo;
        _server.Start();
    }

    private void LogInfo(string message)
    { 
        Debug.Log(message);
    }

    public void OnDestroy()
    {
        _server.Stop();
    }
}

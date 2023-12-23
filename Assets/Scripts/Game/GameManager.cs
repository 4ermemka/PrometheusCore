using Assets.Scripts.Shared.Constants;
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
        #region Server

        _server = new Server();
        _server.OnLog += LogInfo;
        _server.Start();

        #endregion

        #region Statics

        PlayerEffects.GetConfig();

        Buff buff = PlayerEffects.GetBuff("Баффаут");
        LogInfo($"Duration : {buff.Duration}");

        #endregion
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

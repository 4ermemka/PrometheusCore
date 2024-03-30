using Assets.Scripts.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Server
{
    public Action<int,byte[]> OnReceiveMessage;

    public Action<int> OnUserConnected;
    public Action<int> OnUserDisconnected;

    private TcpListener _listener;
    private int _port = 3535;
    private CancellationTokenSource _listenNewClientsCancellationTokenSource;
    private CancellationToken _listenNewClientsCancellationToken;

    private Dictionary<int, TcpClient> _connectedClientsList;

    public Server() 
    {
        _connectedClientsList = new Dictionary<int, TcpClient>();
        _listenNewClientsCancellationTokenSource = new CancellationTokenSource();
        _listenNewClientsCancellationToken = _listenNewClientsCancellationTokenSource.Token;
    }
    public void Start()
    {
        _listener = new TcpListener(IPAddress.Parse("127.0.0.28"), _port);
        try
        {
            _listener.Start();
            ConsoleLogger.LogInformation("Server", $"Server on {_listener.Server.LocalEndPoint} started");
            StartAcceptingNewConnections();
            ConsoleLogger.LogInformation("Server", $"Server on {_listener.Server.LocalEndPoint} now ready for work!");
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogException("Server", ex);
        }
    }

    public void Stop()
    {
        _listenNewClientsCancellationTokenSource.Cancel();
        _listener?.Stop();
        foreach(var clientKey in _connectedClientsList.Keys) 
        {
            _connectedClientsList[clientKey].Close();
        }

        _connectedClientsList.Clear();
        ConsoleLogger.LogInformation("Server", $"Server stopped");
    }

    public void SendClient(int id, byte[] bytes)
    {
        var clientStream = _connectedClientsList[id]?.GetStream();
        clientStream?.Write(bytes);
        ConsoleLogger.LogInformation("Server", $"Sending client{id}: {bytes}");
    }

    public void SendClients(byte[] bytes)
    {
        foreach (var client in _connectedClientsList)
        {
            SendClient(client.Key, bytes);
        }
    }
    
    private void StartAcceptingNewConnections()
    {
        Task.Run(() =>
        {
            while (!_listenNewClientsCancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    ConsoleLogger.LogInformation("Server", $"Accepting new clients...");
                    var client = _listener.AcceptTcpClient();
                    ConsoleLogger.LogInformation("Server", $"Got! {client.Connected}");
                    ConsoleLogger.LogInformation("Server", $"Accepted client : {client.Client.RemoteEndPoint}");
                    int id = FirstAvailableClientId();
                    _connectedClientsList.Add(id, client);
                    StartListenToClient(id, client);
                    OnUserConnected?.Invoke(id);
                }
                catch (Exception ex) 
                { 
                    ConsoleLogger.LogException("Server", ex);
                }
            }
            ConsoleLogger.LogInformation("Server", $"Accepting new clients ended");
        }, _listenNewClientsCancellationToken);
    }

    private void StartListenToClient(int id, TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        CancellationTokenSource _listenClientCancellationTokenSource = new CancellationTokenSource();
        CancellationToken _listenClientCancellationToken = _listenClientCancellationTokenSource.Token;
        Task.Run(() =>
        {
            ConsoleLogger.LogInformation("Server", $"Starting reading messages from {client.Client.RemoteEndPoint}");
            while (!_listenClientCancellationTokenSource.IsCancellationRequested && client.Connected)
            {
                ConsoleLogger.LogInformation("Server", $"Reading...");
                try
                {
                    if (stream != null && stream.CanRead)
                    {
                        int numberOfBytesRead = 0;
                        byte[] myReadBuffer = new byte[4096];
                        StringBuilder myCompleteMessage = new StringBuilder();
                        do
                        {
                            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        }
                        while (stream.DataAvailable && numberOfBytesRead != 0);
                        ConsoleLogger.LogInformation("Server", $"{numberOfBytesRead} bytes received");
                        OnReceiveMessage?.Invoke(id, myReadBuffer);
                        if (numberOfBytesRead == 0)
                        {
                            ConsoleLogger.LogInformation("Server", $"Client [{id}][{client.Client.RemoteEndPoint}] disconnected!");
                            _listenClientCancellationTokenSource.Cancel();
                            client.Close();
                            _connectedClientsList.Remove(id);
                            OnUserDisconnected?.Invoke(id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ConsoleLogger.LogException($"Server, Client [{id}][{client.Client.RemoteEndPoint}]", ex);
                    _listenClientCancellationTokenSource.Cancel();
                    client.Close();
                    _connectedClientsList.Remove(id);
                    OnUserDisconnected?.Invoke(id);
                }
            }
        }, _listenClientCancellationToken);
    }

    private int FirstAvailableClientId()
    {
        int i = 0;
        while (_connectedClientsList.ContainsKey(i))
        {
            //ConsoleLogger.LogInformation("Server", $"Id: {i} incorrect");
            i++;
        }
        //ConsoleLogger.LogInformation("Server", $"Id: {i} correct!");
        return i;
    }

}
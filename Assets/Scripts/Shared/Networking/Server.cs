using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Server
{
    public Action<string> OnLog;
    public Action<int,byte[]> OnReceiveMessage;
    public Action<int> OnNewClientConnected;

    private TcpListener _listener;
    private int _port = 3535;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    private Dictionary<int, TcpClient> _connectedClientsList;

    public Server() 
    {
        _connectedClientsList = new Dictionary<int, TcpClient>();
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }
    public void Start()
    {
        _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
        try
        {
            _listener.Start();
            OnLog?.Invoke($"Server on {_listener.Server.LocalEndPoint} started");
            StartAcceptingNewConnections();
        }
        catch (Exception ex)
        {
            OnLog?.Invoke($"Server exception {ex.Message} : {ex.StackTrace}");
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _listener?.Stop();
        OnLog?.Invoke($"Server stopped");
    }

    public void SendClient(int id, byte[] bytes)
    {
        var clientStream = _connectedClientsList[id]?.GetStream();
        clientStream?.Write(bytes);
        OnLog?.Invoke($"Sending client{id}: {bytes}");
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
            while (!_cancellationToken.IsCancellationRequested)
            {
                OnLog?.Invoke($"Accepting new clients...");
                var client = _listener.AcceptTcpClient();
                OnLog?.Invoke($"Accepted client : {client.Client.RemoteEndPoint}");
                int id = FirstAvailableClientId();
                _connectedClientsList.Add(id, client);
                StartListenToClient(id, client);
            }
        }, _cancellationToken);
    }

    private void StartListenToClient(int id, TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        Task.Run(() =>
        {
            OnLog?.Invoke($"Starting reading messages from {client.Client.RemoteEndPoint}");
            while (!_cancellationTokenSource.IsCancellationRequested && client.Connected)
            {
                OnLog?.Invoke($"Reading...");
                try
                {
                    if (stream.CanRead)
                    {
                        byte[] myReadBuffer = new byte[1024];
                        StringBuilder myCompleteMessage = new StringBuilder();
                        int numberOfBytesRead = 0;
                        do
                        {
                            numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        }
                        while (stream.DataAvailable);
                        OnLog?.Invoke($"{numberOfBytesRead} bytes received");
                        OnReceiveMessage?.Invoke(id, myReadBuffer);
                    }
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke($"Client [{id}] Address:[{client.Client.RemoteEndPoint}] caused exception: {ex.Message}");
                    _cancellationTokenSource.Cancel();
                    client.Close();
                    _connectedClientsList.Remove(id);
                }
            }
        }, _cancellationToken);
    }

    private int FirstAvailableClientId()
    {
        int i = 0;
        while (_connectedClientsList.ContainsKey(i))
        {
            OnLog?.Invoke($"Id: {i} incorrect");
            i++;
        }
        OnLog?.Invoke($"Id: {i} correct!");
        return i;
    }

}
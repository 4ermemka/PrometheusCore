using Assets.Scripts.Shared.Tools;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Client
{
    public Action OnConnectedToServer;
    public Action OnConnectionToServerRefused;
    public Action OnDisconnectedFromServer;

    public Action<byte[]> OnReceiveMessage;

    public string ConnectionAddress;

    private TcpClient _client;
    private int _port = 3535;
    private NetworkStream _stream;

    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    public Client()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        ConnectionAddress = $"EmptyConnectionAddress";
    }

    public void Connect(string ip, int port)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        try
        {
            ConsoleLogger.LogInformation("Client", $"Connecting to server: {ip}:{port}");
            _client = new TcpClient();
            _client.Connect(ip, port);
            ConnectionAddress = $"{ip}:{port}";
            ConsoleLogger.LogInformation("NetManager", $"Client connection complete, checking...");

            if (_client.Connected)
            {
                _stream = _client.GetStream();
                ConsoleLogger.LogInformation("Client", $"Connected to server: {ConnectionAddress} Client connected {_client.Connected}, address: {_client.Client.LocalEndPoint}");
                OnConnectedToServer?.Invoke();
                StartListeningToServer();
            }
            else
            {
                ConsoleLogger.LogWarning("Client", $"Client NOT connected {_client.Connected}, address: {_client.Client.LocalEndPoint}");
            }
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogException("Client", ex);
            OnConnectionToServerRefused?.Invoke();
        }
    }

    public void SendMessage(byte[] bytes)
    {
        _stream.Write(bytes);
        ConsoleLogger.LogInformation("Client", $"Sending {_client?.Client.RemoteEndPoint} bytes [{bytes.Length}]");
    }

    public void Stop()
    { 
        _client?.Close();
        _client?.Dispose();
        ConsoleLogger.LogInformation("Client", $"Client stopped");
        OnDisconnectedFromServer?.Invoke();
        ConnectionAddress = $"EmptyConnectionAddress";
    }

    private void StartListeningToServer()
    {
        Task.Run(() =>
        {
            ConsoleLogger.LogInformation("Client", $"Starting reading messages from server {_client.Client.RemoteEndPoint}");
            while (!_cancellationTokenSource.IsCancellationRequested && _client.Connected)
            {
                ConsoleLogger.LogInformation("Client", $"Reading...");
                try
                {
                    if (_stream.CanRead)
                    {
                        byte[] myReadBuffer = new byte[4096];
                        StringBuilder myCompleteMessage = new StringBuilder();
                        int numberOfBytesRead = 0;
                        do
                        {
                            numberOfBytesRead = _stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        }
                        while (_stream.DataAvailable);
                        ConsoleLogger.LogInformation("Client", $"{numberOfBytesRead} bytes received");
                        OnReceiveMessage?.Invoke(myReadBuffer);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleLogger.LogWarning("Client", $"Disconnection due to: {ex.Message}");
                    _cancellationTokenSource.Cancel();
                    Stop();
                }
            }
        }, _cancellationToken);
    }
}

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Client
{
    public Action<string> OnLog;
    public Action<byte[]> OnReceiveMessage;

    private TcpClient _client;
    private int _port = 3535;
    private NetworkStream _stream;

    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    public Client()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    public void Connect(string ip, int port)
    {
        try
        {
            _client = new TcpClient();
            _client.Connect(ip, port);
            _stream = _client.GetStream();
            OnLog?.Invoke($"Client connected {_client.Connected}, address: {_client.Client.LocalEndPoint}");
            StartListeningToServer();
        }
        catch (Exception ex)
        {
            OnLog?.Invoke($"Client exception: {ex.Message}");
        }
    }

    public void SendMessage(byte[] bytes)
    {
        _stream.Write(bytes);
        OnLog?.Invoke($"Sending {_client?.Client.RemoteEndPoint} bytes [{bytes.Length}]");
    }

    public void Stop()
    { 
        _client?.Close();
        _client?.Dispose();
        OnLog?.Invoke($"Client stopped");
    }

    private void StartListeningToServer()
    {
        Task.Run(() =>
        {
            OnLog?.Invoke($"Starting reading messages from server {_client.Client.RemoteEndPoint}");
            while (!_cancellationTokenSource.IsCancellationRequested && _client.Connected)
            {
                OnLog?.Invoke($"Reading...");
                try
                {
                    if (_stream.CanRead)
                    {
                        byte[] myReadBuffer = new byte[1024];
                        StringBuilder myCompleteMessage = new StringBuilder();
                        int numberOfBytesRead = 0;
                        do
                        {
                            numberOfBytesRead = _stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        }
                        while (_stream.DataAvailable);
                        OnLog?.Invoke($"{numberOfBytesRead} bytes received");
                        OnReceiveMessage?.Invoke(myReadBuffer);
                    }
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke($"Disconnection due to: {ex.Message}");
                    _cancellationTokenSource.Cancel();
                    Stop();
                }
            }
        }, _cancellationToken);
    }
}

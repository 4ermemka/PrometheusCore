using Assets.Scripts.Shared.Abstraction;
using System;
using System.Collections.Concurrent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shared.View
{
    internal enum MessageType
    { 
        Simple,
        Warning,
        Error,
        Exception
    }

    internal class SimpleConsoleMessage
    { 
        public MessageType Type;
        public DateTime Time;
        public string Source;
        public string Message;

        public SimpleConsoleMessage(MessageType type, DateTime time, string source, string message)
        { 
            Type = type; 
            Time = time; 
            Source = source; 
            Message = message;
        }
    }

    [Serializable]
    public class SimpleViewConsole : MonoBehaviour, IConsole
    {
        [SerializeField]
        private TextMeshProUGUI ConsoleText;

        ConcurrentQueue<SimpleConsoleMessage> m_queuedLogs = new ConcurrentQueue<SimpleConsoleMessage>();

        public void Start()
        {
            ConsoleText.text = $" ============ Console ============ ";
        }

        public void Update()
        {
            m_queuedLogs.TryDequeue(out SimpleConsoleMessage newMsg);
            
            if (newMsg != null)
            { 
                string message = $"[{newMsg.Time.ToString("HH:mm:ss:fff")}]\n({newMsg.Source}) > {newMsg.Message}";
                switch (newMsg.Type)
                {
                    case MessageType.Simple:
                        message = $"<color=#ffffff>{message}</color>";
                        break;

                    case MessageType.Warning:
                        message = $"<color=#ffc000>{message}</color>";
                        break;

                    case MessageType.Error:
                        message = $"<color=#ff8800>{message}</color>";
                        break; 

                    case MessageType.Exception:
                        message = $"<color=#ff0000>{message}</color>";
                        break;
                }

                ConsoleText.text += $"\n\n{message}";
            }
        }

        public void LogInformation(string source, string message)
        {
            m_queuedLogs.Enqueue(new SimpleConsoleMessage(MessageType.Simple, DateTime.Now, source, message));
        }

        public void LogWarning(string source, string message)
        {
            m_queuedLogs.Enqueue(new SimpleConsoleMessage(MessageType.Warning, DateTime.Now, source, message));

        }

        public void LogError(string source, string message)
        {
            m_queuedLogs.Enqueue(new SimpleConsoleMessage(MessageType.Error, DateTime.Now, source, message));
        }

        public void LogException(string source, Exception ex)
        {
            m_queuedLogs.Enqueue(new SimpleConsoleMessage(MessageType.Exception, DateTime.Now, source, $"\nEX: {ex.Message},\n INNER EX: {ex.InnerException?.Message} \nTRACE: {ex.StackTrace}"));
        }

        public void ClearConsole()
        {
            ConsoleText.text = "> Консоль очищена!";
        }
    }
}

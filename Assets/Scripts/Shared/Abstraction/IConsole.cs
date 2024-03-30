using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Abstraction
{
    public interface IConsole
    {
        void LogInformation(string source, string message);
        void LogWarning(string source, string message);
        void LogError(string source, string message);
        void LogException (string source, Exception ex);
    }
}

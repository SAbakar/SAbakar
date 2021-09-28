
namespace Contracts
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogErrorNull(string ActionName);
        void LogErrorNonValide(string ActionName);
        void LogInfo(string message);
        void LogWarn(string message);
    }
}

namespace BlogManager.Core;

public interface IBlogManagerLogger
{
    void LogInformation(string message);
    void LogInformation(string message, params object?[] args);
    void LogWarning(string     message);
    void LogError(string       message);
    void LogError(string?      message, params object?[] args);
}
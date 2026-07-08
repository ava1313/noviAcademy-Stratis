using WorldRank.Interfaces;

namespace WorldRank.Classes;

public class  ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - { message}");

    }
    public void LogWarning(string message) {
        Console.WriteLine($"[WARNING] {DateTime.Now:HH:mm:ss} - { message}");
    }   

    public void LogError(string message)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - { message}");
    }

    public void LogError(string message, Exception exception)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        Console.WriteLine($"Exception: {exception.Message}");
    }

}
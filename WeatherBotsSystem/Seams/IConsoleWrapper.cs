namespace WeatherBots.Seams;

public interface IConsoleWrapper
{
    Task WriteLineAsync(string message);
    Task<string?> ReadLineAsync();
}

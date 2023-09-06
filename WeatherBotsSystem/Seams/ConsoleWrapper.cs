namespace WeatherBots.Seams;

public class ConsoleWrapper : IConsoleWrapper
{
    public Task<string?> ReadLineAsync() => Console.In.ReadLineAsync();

    public Task WriteLineAsync(string message) => Console.Out.WriteLineAsync(message);
}


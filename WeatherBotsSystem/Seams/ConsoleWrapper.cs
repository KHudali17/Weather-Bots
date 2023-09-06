namespace WeatherBots.Seams;

public class ConsoleWrapper : IConsoleWrapper
{
    public Task WriteLineAsync(string message) => Console.Out.WriteLineAsync(message);
}


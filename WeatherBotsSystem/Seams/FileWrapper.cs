namespace WeatherBots.Seams;

public class FileWrapper : IFileWrapper
{
    public bool Exists(string? path) => File.Exists(path);
}

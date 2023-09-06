namespace WeatherBots.Seams;

public interface IFileWrapper
{
    bool Exists(string? path);
}

namespace WeatherBots.Seams;

public interface IFileStreamWrapper
{
    Stream GetAsyncStream(string filePath);
}

namespace WeatherBots.Seams;

public class FileStreamWrapper : IFileStreamWrapper
{
    public Stream GetAsyncStream(string filePath)
        => new FileStream(filePath, FileMode.Open, FileAccess.Read,
                FileShare.Read, bufferSize: 4096, useAsync: true);
}

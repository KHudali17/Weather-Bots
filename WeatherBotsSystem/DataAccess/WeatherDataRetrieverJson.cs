using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBots.DataAccess;

public class WeatherDataRetrieverJson : IWeatherDataRetriever
{
    private readonly string _source;
    private readonly IFileStreamWrapper _fileStreamWrapper;

    public WeatherDataRetrieverJson(string source, IFileStreamWrapper fileStreamWrapper)
    {
        _source = source;
        _fileStreamWrapper = fileStreamWrapper;
    }

    public async Task<WeatherData> GetWeatherData()
    {
        var weatherDataDeserialized = await JsonFileReader.ReadJsonFileAsync<WeatherData>(
            _fileStreamWrapper.GetAsyncStream(_source));
        return weatherDataDeserialized;
    }
}

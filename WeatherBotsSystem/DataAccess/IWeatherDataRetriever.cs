using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess;

public interface IWeatherDataRetriever
{
    Task<WeatherData> GetWeatherData();
}

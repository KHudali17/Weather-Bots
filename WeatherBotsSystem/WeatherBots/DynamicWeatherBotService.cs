using WeatherBots.DataRecords;
using WeatherBots.ObserversAndPublishers;
using WeatherBots.Seams;
using WeatherBots.WeatherBots;

namespace WeatherBots;

public class DynamicWeatherBotService
{
    private List<DynamicWeatherBot> _dynamicWeatherBots = new();
    private readonly IWeatherBotFactory _weatherBotFactory;

    public DynamicWeatherBotService(IWeatherBotFactory weatherBotFactory)
    {
        _weatherBotFactory = weatherBotFactory;
    }

    public void InstantiateBots()
    {
        _dynamicWeatherBots = new List<DynamicWeatherBot>
        {
            _weatherBotFactory.CreateSnowBot()!,
            _weatherBotFactory.CreateSunBot()!,
            _weatherBotFactory.CreateRainBot()!
        }
        .Where(bot => bot != null)
        .ToList();
    }

    public bool AddBotsToPublisher<T>(Publisher<T> publisher)
    {
        _dynamicWeatherBots.ForEach(bot => publisher.AddObserver((IPublisherObserver<T>)bot));
        return true;
    }
}


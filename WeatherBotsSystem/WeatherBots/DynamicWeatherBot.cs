using WeatherBots.DataRecords;
using WeatherBots.ObserversAndPublishers;
using WeatherBots.Seams;

namespace WeatherBots.WeatherBots;

public abstract class DynamicWeatherBot : IWeatherBot, IPublisherObserver<WeatherData>
{
    protected BotSettings _settings;
    protected WeatherData? _data;
    protected IConsoleWrapper _consoleWrapper;

    public DynamicWeatherBot(
        BotSettings settings,
        IConsoleWrapper consoleWrapper,
        WeatherData? data = null
        )
    {
        _settings = settings;
        _data = data;
        _consoleWrapper = consoleWrapper;
    }

    public abstract Task<bool> ExecuteBotAction();

    public virtual Task<bool> Update(WeatherData data)
    {
        _data = data;
        return ExecuteBotAction();
    }
}


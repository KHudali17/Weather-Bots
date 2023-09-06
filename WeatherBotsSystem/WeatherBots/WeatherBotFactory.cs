using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBots.WeatherBots;

public class WeatherBotFactory : IWeatherBotFactory
{
    private readonly BotConfig _botConfigs;
    private readonly IConsoleWrapper _consoleWrapper;

    public WeatherBotFactory(BotConfig botConfigs, IConsoleWrapper consoleWrapper)
    {
        _botConfigs = botConfigs;
        _consoleWrapper = consoleWrapper;
    }
    public DynamicWeatherBot? CreateRainBot()
        => _botConfigs.RainBot.Enabled ? new RainBot(_botConfigs.RainBot, _consoleWrapper) : null;

    public DynamicWeatherBot? CreateSnowBot()
        => _botConfigs.SnowBot.Enabled ? new SnowBot(_botConfigs.SnowBot, _consoleWrapper) : null;

    public DynamicWeatherBot? CreateSunBot()
        => _botConfigs.SunBot.Enabled ? new SunBot(_botConfigs.SunBot, _consoleWrapper) : null;
}


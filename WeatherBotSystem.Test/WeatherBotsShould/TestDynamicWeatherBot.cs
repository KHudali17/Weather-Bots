using WeatherBots.DataRecords;
using WeatherBots.Seams;
using WeatherBots.WeatherBots;

namespace WeatherBotsSystem.Test.WeatherBotsShould;

public class TestDynamicWeatherBot : DynamicWeatherBot
{
    public TestDynamicWeatherBot(BotSettings settings,
        IConsoleWrapper consoleWrapper,
        WeatherData? data = null)
        : base(settings, consoleWrapper, data)
    {
    }

    public WeatherData? Data => _data;

    public override Task<bool> ExecuteBotAction()
    {
        throw new NotImplementedException();
    }
}



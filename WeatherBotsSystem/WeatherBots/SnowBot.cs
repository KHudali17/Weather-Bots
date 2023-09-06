using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBots.WeatherBots;

public class SnowBot : DynamicWeatherBot
{
    public SnowBot(BotSettings settings,
        IConsoleWrapper consoleWrapper,
        WeatherData? data = null) : base(settings, consoleWrapper, data) { }

    public override async Task<bool> ExecuteBotAction()
    {
        if (_data == null) throw new DataUnavailableException();
        if (_settings.TemperatureThreshold == null) return false;
        if (_settings.TemperatureThreshold <= _data.Temperature) return false;

        await _consoleWrapper.WriteLineAsync("SnowBot activated");
        await _consoleWrapper.WriteLineAsync(_settings.Message);

        return true;
    }
}


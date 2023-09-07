using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBots.WeatherBots;

public class RainBot : DynamicWeatherBot
{
    public RainBot(BotSettings settings,
        IConsoleWrapper consoleWrapper,
        WeatherData? data = null) : base(settings, consoleWrapper, data) { }

    public override async Task<bool> ExecuteBotAction()
    {
        if (_data == null) throw new DataUnavailableException();
        if (_settings.HumidityThreshold == null) return false;
        if (_settings.HumidityThreshold >= _data.Humidity) return false;

        await _consoleWrapper.WriteLineAsync("RainBot activated");
        await _consoleWrapper.WriteLineAsync(_settings.Message);

        return true;
    }
}


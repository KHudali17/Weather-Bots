namespace WeatherBots.WeatherBots;

public interface IWeatherBot
{
    Task<bool> ExecuteBotAction();
}

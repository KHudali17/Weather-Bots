namespace WeatherBots.WeatherBots;

public interface IWeatherBotFactory
{
    public DynamicWeatherBot? CreateSunBot();
    public DynamicWeatherBot? CreateSnowBot();
    public DynamicWeatherBot? CreateRainBot();
}


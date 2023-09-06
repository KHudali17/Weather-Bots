namespace WeatherBots.DataRecords;

public record BotConfig
{
    public required BotSettings SunBot { get; init; }
    public required BotSettings RainBot { get; init; }
    public required BotSettings SnowBot { get; init; }
}

using System.Reflection;
using System.Text.Json;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess;

public class ConfigRetrieverJson
{
    public static async Task<BotConfig> GetConfigFromJson(Stream stream)
    {
        var botConfigDeserialized = await JsonFileReader.ReadJsonFileAsync<BotConfig>(stream);

        if (!IsValidBotConfig(botConfigDeserialized)) throw new JsonException();

        return botConfigDeserialized;
    }

    private static bool IsValidBotConfig(BotConfig botConfig)
    {
        PropertyInfo[] botConfigProperties = typeof(BotConfig).GetProperties();

        return botConfigProperties
               .Select(property => property.GetValue(botConfig))
               .OfType<BotSettings>()
               .All(botSettings => botSettings.HasExactlyOneThreshold);
    }
}

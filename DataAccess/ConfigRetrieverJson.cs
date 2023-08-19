using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess
{
    public class ConfigRetrieverJson
    {
        public static async Task<BotConfig> GetConfigFromJson(string fileName)
        {
            var botConfigDeserialized = await JsonFileReader.ReadJsonFileAsync<BotConfig>(fileName);
            
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
}

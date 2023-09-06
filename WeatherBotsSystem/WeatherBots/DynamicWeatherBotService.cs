using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;
using WeatherBots.ObserversAndPublishers;
using WeatherBots.WeatherBots;

namespace WeatherBots
{
    public class DynamicWeatherBotService
    {
        private List<DynamicWeatherBot> _dynamicWeatherBots = new();

        public DynamicWeatherBotService(BotConfig configs, Publisher<WeatherData> weatherPublisher) 
        {
            instantiateBotsFromConfig(configs);
            AddToPublisher(weatherPublisher);
        }

        private void instantiateBotsFromConfig(BotConfig configs)
        {
            var configPropertyToBotMapping = new Dictionary<BotSettings, DynamicWeatherBot>()
            {
                { configs.SunBot, new SunBot(configs.SunBot) },
                { configs.RainBot, new RainBot(configs.RainBot) },
                { configs.SnowBot, new SnowBot(configs.SnowBot) }
            };

            _dynamicWeatherBots = configPropertyToBotMapping
                                  .Where(kv => kv.Key.Enabled)
                                  .Select(kv => kv.Value)
                                  .ToList();
        }

        private bool AddToPublisher<T>(Publisher<T> publisher)
        {
            _dynamicWeatherBots.ForEach(bot => publisher.AddObserver((IPublisherObserver<T>)bot));
            return true;
        }
    }
}

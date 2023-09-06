using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.WeatherBots
{
    public class RainBot : DynamicWeatherBot
    {
        public RainBot(BotSettings settings, WeatherData? data = null) : base(settings, data) { }

        public override async Task<bool> ExecuteBotAction()
        {
            if (_data == null) throw new DataUnavailableException();
            if (_settings.HumidityThreshold == null) return false;
            if (_settings.HumidityThreshold >= _data.Humidity) return false;

            await Console.Out.WriteLineAsync("RainBot activated");
            await Console.Out.WriteLineAsync(_settings.Message);

            return true;
        }
    }
}

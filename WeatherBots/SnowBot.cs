using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.WeatherBots
{
    public class SnowBot : DynamicWeatherBot
    {
        public SnowBot(BotSettings settings, WeatherData? data = null) : base(settings, data) { }

        public override async Task<bool> ExecuteBotAction()
        {
            if (_data == null) throw new DataUnavailableException();
            if (_settings.TemperatureThreshold == null) return false;
            if (_settings.TemperatureThreshold <= _data.Temperature) return false;

            await Console.Out.WriteLineAsync("SnowBot activated");
            await Console.Out.WriteLineAsync(_settings.Message);
            
            return true;
        }
    }
}

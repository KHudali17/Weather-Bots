using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;
using WeatherBots.ObserversAndPublishers;

namespace WeatherBots.WeatherBots
{
    public abstract class DynamicWeatherBot : IWeatherBot, IPublisherObserver<WeatherData>
    {
        protected BotSettings _settings;
        protected WeatherData? _data;

        public DynamicWeatherBot(BotSettings settings, WeatherData? data = null)
        {
            _settings = settings;
            _data = data;
        }

        public abstract Task<bool> ExecuteBotAction();

        public virtual Task<bool> Update(WeatherData data)
        {
            _data = data;
            return ExecuteBotAction();
        }
    }
}

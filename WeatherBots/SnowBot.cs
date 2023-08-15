using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.WeatherBots
{
    public class SnowBot : IDynamicWeatherBot
    {
        public Task ExecuteBotAction()
        {
            throw new NotImplementedException();
        }

        public Task Update(IWeatherData data)
        {
            throw new NotImplementedException();
        }

        public Task Update(IConfigData data)
        {
            throw new NotImplementedException();
        }
    }
}

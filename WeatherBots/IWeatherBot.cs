using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.ObserversAndPublishers;

namespace WeatherBots.WeatherBots
{
    public interface IWeatherBot 
    {
        Task ExecuteBotAction();
    }
}

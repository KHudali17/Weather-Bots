using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverJson : IWeatherDataRetriever
    {
        public Task<IWeatherData> GetWeatherData()
        {
            throw new NotImplementedException();
        }
    }
}

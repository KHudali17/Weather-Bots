using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverXml : IWeatherDataRetriever
    {
        public Task<IWeatherData> GetWeatherData()
        {
            throw new NotImplementedException();
        }
    }
}
